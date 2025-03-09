import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:namesa/core/api_services.dart';
import 'package:namesa/reservation/view/manager/reserve_provider.dart';
import 'package:provider/provider.dart';

class ReservationsScreen extends StatefulWidget {
  const ReservationsScreen({super.key});

  @override
  State<ReservationsScreen> createState() => _ReservationsScreenState();
}

class _ReservationsScreenState extends State<ReservationsScreen> {
  void _showReservationDialog(
      BuildContext context, ReserveProvider provider, int reservationId) {
    showDialog(
      context: context,
      builder: (context) {
        final _formKey = GlobalKey<FormState>();
        TextEditingController checkInController =
            TextEditingController(text: "${provider.result.data!.from}");
        TextEditingController checkOutController =
            TextEditingController(text: "${provider.result.data!.to}");
        String? selectedPaymentMethod;
        selectedPaymentMethod =
            provider.result.data!.paymentMethod == 1 ? "Cash" : "Credit Card";
        return AlertDialog(
          title: Text("Update Reservation",
              style: TextStyle(color: Color(0xffBE7C01))),
          content: Form(
            key: _formKey,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Text("Reservation ID: ${reservationId}",
                    style: TextStyle(color: Color(0xffBE7C01))),

                // Check-in Date
                TextFormField(
                  controller: checkInController,
                  decoration: InputDecoration(
                    // hintText: "${provider.result.data!.from}",
                    hintStyle: TextStyle(color: Color(0xffBE7C01)),
                    suffixIcon: Icon(Icons.calendar_today),
                  ),
                  readOnly: true,
                  onTap: () => _selectDate(context, checkInController),
                  validator: (value) =>
                      value!.isEmpty ? "Please select check-in date" : null,
                ),

                // Check-out Date
                TextFormField(
                  controller: checkOutController,
                  decoration: InputDecoration(
                    // hintText: "${provider.result.data!.to}",
                    hintStyle: TextStyle(color: Color(0xffBE7C01)),
                    suffixIcon: Icon(Icons.calendar_today),
                  ),
                  readOnly: true,
                  onTap: () => _selectDate(context, checkOutController),
                  validator: (value) =>
                      value!.isEmpty ? "Please select check-out date" : null,
                ),

                // Payment Method Dropdown
                DropdownButtonFormField<String>(
                  value:
                      "${provider.result.data!.paymentMethod == 1 ? "Cash" : "Credit Card"}",
                  decoration: InputDecoration(
                    labelText: "Payment Method",
                    labelStyle: TextStyle(color: Color(0xffBE7C01)),
                  ),
                  items: ["Cash", "Credit Card"]
                      .map((method) => DropdownMenuItem(
                            value: method,
                            child: Text(method),
                          ))
                      .toList(),
                  onChanged: (value) {
                    setState(() {
                      selectedPaymentMethod = value;
                    });
                  },
                  validator: (value) =>
                      value == null ? "Please select a payment method" : null,
                ),
              ],
            ),
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("Cancel", style: TextStyle(color: Color(0xffBE7C01))),
            ),
            TextButton(
              onPressed: () async {
                if (_formKey.currentState!.validate()) {
                  String checkInDate = checkInController.text;
                  String checkOutDate = checkOutController.text;
                  int paymentMethod = selectedPaymentMethod == "Cash" ? 1 : 2;

                  await provider.updateReservation(
                      reservationId, checkInDate, checkOutDate, paymentMethod);
                  await provider.getAllReservations();
                  await ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(
                      content: Text(provider.message),
                      behavior: SnackBarBehavior.fixed,
                      dismissDirection: DismissDirection.startToEnd,
                    ),
                  );

                  Navigator.pop(context);
                }
              },
              child: Text("Update", style: TextStyle(color: Color(0xffBE7C01))),
            ),
          ],
        );
      },
    );
  }

  Future<void> _selectDate(
      BuildContext context, TextEditingController controller) async {
    DateTime? pickedDate = await showDatePicker(
      context: context,
      initialDate: DateTime.now(),
      firstDate: DateTime.now(),
      lastDate: DateTime(2100),
    );
    if (pickedDate != null) {
      setState(() {
        controller.text =
            "${pickedDate.year}-${pickedDate.month.toString().padLeft(2, '0')}-${pickedDate.day.toString().padLeft(2, '0')}";
      });
    }
  }

  @override
  void initState() {
    super.initState();
    Future.microtask(() => Provider.of<ReserveProvider>(context, listen: false)
        .getAllReservations());
  }

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(create: (context) {
      final provider = ReserveProvider(ApiService(Dio()));
      provider.getAllReservations();
      return provider;
    }, child: Consumer<ReserveProvider>(builder: (context, value, child) {
      print(value.reservations.length);
      return Scaffold(
        backgroundColor: Colors.black,
        appBar: AppBar(
          title: Text(
            "Reservations",
            style: TextStyle(color: Colors.white),
          ),
          centerTitle: true,
          backgroundColor: Colors.black,
          iconTheme: IconThemeData(color: Colors.white),
        ),
        body: value.isLoading
            ? Center(child: CircularProgressIndicator())
            : Padding(
                padding: EdgeInsets.all(10.sp),
                child: value.reservations.length == 0
                    ? Center(
                        child: Text(
                          "No reservations yet!",
                          style:
                              TextStyle(color: Colors.white, fontSize: 20.sp),
                        ),
                      )
                    : ListView(
                        shrinkWrap: true,
                        children: [
                          Text("All Reservations",
                              style: TextStyle(
                                  fontWeight: FontWeight.bold,
                                  color: Colors.white,
                                  fontSize: 20.sp)),
                          SizedBox(
                            height: 10.h,
                          ),
                          ...List.generate(
                              value.reservations.length,
                              (i) => InkWell(
                                    onTap: () async {
                                      await value.getReservationDetails(value
                                          .reservations[i]['reservationId']);
                                      await showDialog(
                                          context: context,
                                          builder: (context) => AlertDialog(
                                                title: Text("Details"),
                                                content: Column(
                                                  crossAxisAlignment:
                                                      CrossAxisAlignment.start,
                                                  mainAxisSize:
                                                      MainAxisSize.min,
                                                  children: [
                                                    Text(
                                                        "Owner: ${value.result.data!.guestName!}"),
                                                    Text(
                                                        "Price: ${value.result.data!.totalPrice!}"),
                                                    Text(
                                                        "From: ${value.result.data!.from!}"),
                                                    Text(
                                                        "To: ${value.result.data!.to!}"),
                                                    Text(
                                                        "Total Days: ${value.result.data!.totalNumberOfDays!}"),
                                                    Text(
                                                        "Payment Method: ${value.result.data!.paymentMethod == 1 ? "Cash" : "Credit Card"}"),
                                                    Text(
                                                        "Status: ${value.result.data!.reservationStatus == 1 ? "Pending" : value.result.data!.reservationStatus == 2 ? "Approved" : "Rejected"}"),
                                                  ],
                                                ),
                                              ));
                                    },
                                    child: Padding(
                                      padding:
                                          EdgeInsets.symmetric(vertical: 5.h),
                                      child: ListTile(
                                        tileColor: Colors.white,
                                        title: Text(
                                          "Room: ${value.reservations[i]['reservationId']}",
                                          style: TextStyle(
                                              fontWeight: FontWeight.bold),
                                        ),
                                        subtitle: Text(
                                            "Price: ${value.reservations[i]['totalPrice']}",
                                            style: TextStyle(
                                                fontWeight: FontWeight.bold)),
                                        trailing: Row(
                                          mainAxisSize: MainAxisSize.min,
                                          children: [
                                            IconButton(
                                              onPressed: () async {
                                                await value.cancelReservation(
                                                    value.reservations[i]
                                                        ['reservationId']);

                                                await value
                                                    .getAllReservations();
                                                if (value
                                                    .reservations.isEmpty) {
                                                  Future.delayed(
                                                      Duration(
                                                          milliseconds: 100),
                                                      () {
                                                    setState(
                                                        () {}); // Ensure the UI rebuilds
                                                  });
                                                }

                                                if (value.message.isNotEmpty) {
                                                  await ScaffoldMessenger.of(
                                                          context)
                                                      .showSnackBar(
                                                    SnackBar(
                                                      content: Text(
                                                          value.message,
                                                          style: TextStyle(
                                                              color: Colors
                                                                  .white)),
                                                      behavior: SnackBarBehavior
                                                          .floating,
                                                      dismissDirection:
                                                          DismissDirection
                                                              .startToEnd,
                                                    ),
                                                  );
                                                }
                                              },
                                              icon: Icon(
                                                Icons.delete,
                                                color: Colors.black,
                                              ),
                                              style: IconButton.styleFrom(
                                                  backgroundColor: Colors.red),
                                            ),
                                            IconButton(
                                                onPressed: () async {
                                                  await value
                                                      .getReservationDetails(
                                                          value.reservations[i][
                                                              'reservationId']);
                                                  _showReservationDialog(
                                                      context,
                                                      value,
                                                      value.reservations[i]
                                                          ['reservationId']);
                                                },
                                                icon: Icon(Icons.edit)),
                                          ],
                                        ),
                                      ),
                                    ),
                                  ))
                        ],
                      ),
              ),
      );
    }));
  }
}
