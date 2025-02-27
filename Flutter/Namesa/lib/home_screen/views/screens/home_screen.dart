import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:namesa/core/api_services.dart';
import 'package:namesa/core/cached_data.dart';
import 'package:namesa/home_screen/views/manager/room_provider.dart';
import 'package:provider/provider.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  _HomeScreenState createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  void _showCreateRoomDialog(BuildContext context, RoomProvider create) {
    final TextEditingController typeController = TextEditingController();
    final TextEditingController priceController = TextEditingController();
    final TextEditingController capacityController = TextEditingController();
    bool isAvailable = true;

    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setState) {
            return AlertDialog(
              title: const Text("Create Room",
                  style: TextStyle(color: Color(0xffBE7C01))),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextField(
                    controller: typeController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Room Type",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                  ),
                  TextField(
                    controller: priceController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Price",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                    keyboardType: TextInputType.number,
                  ),
                  TextField(
                    controller: capacityController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Number of Beds",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                    keyboardType: TextInputType.number,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text("On Sea",
                          style: TextStyle(color: Color(0xffBE7C01))),
                      Switch(
                        activeColor: const Color(0xffBE7C01),
                        thumbColor: WidgetStateProperty.resolveWith<Color>(
                            (Set<WidgetState> states) {
                          return Colors.white;
                        }),
                        inactiveThumbColor: Colors.white,
                        inactiveTrackColor: Colors.black,
                        value: isAvailable,
                        onChanged: (value) {
                          setState(() {
                            isAvailable = value;
                          });
                        },
                      ),
                    ],
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text("Cancel",
                      style: TextStyle(color: Color(0xffBE7C01))),
                ),
                TextButton(
                  onPressed: () {
                    create.createRoom(
                      typeController.text,
                      double.tryParse(priceController.text) ?? 0.0,
                      int.tryParse(capacityController.text) ?? 0,
                      isAvailable,
                    );
                    Navigator.pop(context);
                  },
                  child: const Text("Create",
                      style: TextStyle(color: Color(0xffBE7C01))),
                ),
              ],
            );
          },
        );
      },
    );
  }

  void _showUpdateRoomDialog(BuildContext context, RoomProvider update) {
    final TextEditingController roomIdController = TextEditingController();
    final TextEditingController typeController = TextEditingController();
    final TextEditingController priceController = TextEditingController();
    final TextEditingController capacityController = TextEditingController();
    bool isAvailable = true;

    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setState) {
            return AlertDialog(
              title: const Text("Update Room",
                  style: TextStyle(color: Color(0xffBE7C01))),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextField(
                    controller: roomIdController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Room Id",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                  ),
                  TextField(
                    controller: typeController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Room Type",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                  ),
                  TextField(
                    controller: priceController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Price",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                    keyboardType: TextInputType.number,
                  ),
                  TextField(
                    controller: capacityController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Number of Beds",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                    keyboardType: TextInputType.number,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text("On Sea",
                          style: TextStyle(color: Color(0xffBE7C01))),
                      Switch(
                        activeColor: const Color(0xffBE7C01),
                        thumbColor: WidgetStateProperty.resolveWith<Color>(
                            (Set<WidgetState> states) {
                          return Colors.white;
                        }),
                        inactiveThumbColor: Colors.white,
                        inactiveTrackColor: Colors.black,
                        value: isAvailable,
                        onChanged: (value) {
                          setState(() {
                            isAvailable = value;
                          });
                        },
                      ),
                    ],
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text("Cancel",
                      style: TextStyle(color: Color(0xffBE7C01))),
                ),
                TextButton(
                  onPressed: () {
                    update.updateRoom(
                      int.tryParse(roomIdController.text)!,
                      typeController.text,
                      double.tryParse(priceController.text) ?? 0.0,
                      int.tryParse(capacityController.text) ?? 0,
                      isAvailable,
                    );
                    Navigator.pop(context);
                  },
                  child: const Text("Update",
                      style: TextStyle(color: Color(0xffBE7C01))),
                ),
              ],
            );
          },
        );
      },
    );
  }

  void _showDeleteRoomDialog(BuildContext context, RoomProvider delete) {
    final TextEditingController roomIdController = TextEditingController();

    showDialog(
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (context, setState) {
            return AlertDialog(
              title: const Text("Delete Room",
                  style: TextStyle(color: Color(0xffBE7C01))),
              content: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextField(
                    controller: roomIdController,
                    decoration: const InputDecoration(
                        enabledBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        focusedBorder: UnderlineInputBorder(
                            borderSide: BorderSide(color: Color(0xffBE7C01))),
                        labelText: "Room Id",
                        labelStyle: TextStyle(color: Color(0xffBE7C01))),
                  ),
                ],
              ),
              actions: [
                TextButton(
                  onPressed: () => Navigator.pop(context),
                  child: const Text("Cancel",
                      style: TextStyle(color: Color(0xffBE7C01))),
                ),
                TextButton(
                  onPressed: () {
                    delete.deleteRoom(int.tryParse(roomIdController.text)!);
                    Navigator.pop(context);
                  },
                  child: const Text("Delete",
                      style: TextStyle(color: Color(0xffBE7C01))),
                ),
              ],
            );
          },
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.black,
      appBar: MyCache.getString(key: "role") == "Staff"
          ? AppBar(
              title: const Text(
                "Staff Management",
                style: TextStyle(color: Color(0xffBE7C01)),
              ),
              centerTitle: true,
              backgroundColor: Colors.white,
            )
          : null,
      body: MyCache.getString(key: "role") == "Staff"
          ? ChangeNotifierProvider(
              create: (context) => RoomProvider(ApiService(Dio())),
              child: Center(
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    Consumer<RoomProvider>(
                      builder: (context, create, child) => SizedBox(
                        width: MediaQuery.of(context).size.width - 50.w,
                        child: ElevatedButton(
                          onPressed: () =>
                              _showCreateRoomDialog(context, create),
                          style: ElevatedButton.styleFrom(
                            padding: EdgeInsets.symmetric(vertical: 10.h),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(50),
                            ),
                          ),
                          child: Text(
                            "Create Room",
                            style: TextStyle(
                              fontSize: 18.sp,
                              color: const Color(0xffBE7C01),
                            ),
                          ),
                        ),
                      ),
                    ),
                    SizedBox(height: 10.h),
                    Consumer<RoomProvider>(
                      builder: (context, update, child) => SizedBox(
                        width: MediaQuery.of(context).size.width - 50.w,
                        child: ElevatedButton(
                          onPressed: () =>
                              _showUpdateRoomDialog(context, update),
                          style: ElevatedButton.styleFrom(
                            padding: EdgeInsets.symmetric(vertical: 10.h),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(50),
                            ),
                          ),
                          child: Text(
                            "Update Room",
                            style: TextStyle(
                                fontSize: 18.sp,
                                color: const Color(0xffBE7C01)),
                          ),
                        ),
                      ),
                    ),
                    SizedBox(height: 10.h),
                    Consumer<RoomProvider>(
                      builder: (context, delete, child) => SizedBox(
                        width: MediaQuery.of(context).size.width - 50.w,
                        child: ElevatedButton(
                          onPressed: () =>
                              _showDeleteRoomDialog(context, delete),
                          style: ElevatedButton.styleFrom(
                            padding: EdgeInsets.symmetric(vertical: 10.h),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(50),
                            ),
                          ),
                          child: Text(
                            "Delete Room",
                            style: TextStyle(
                                fontSize: 18.sp,
                                color: const Color(0xffBE7C01)),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            )
          : Padding(
              padding: const EdgeInsets.all(15),
              child: ListView(
                shrinkWrap: true,
                children: [
                  Text(
                    "Hello, ${MyCache.getString(key: "role")} ",
                    textAlign: TextAlign.start,
                    style:
                        TextStyle(fontSize: 20.sp, fontWeight: FontWeight.bold),
                  ),
                ],
              ),
            ),
    );
  }
}
