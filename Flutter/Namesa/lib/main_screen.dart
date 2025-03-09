import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:namesa/core/api_services.dart';
import 'package:namesa/core/cached_data.dart';
import 'package:namesa/home_screen/views/screens/home_screen.dart';
import 'package:namesa/reservation/view/manager/reserve_provider.dart';
import 'package:namesa/reservation/view/screens/reservations_screen.dart';
import 'package:provider/provider.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({super.key});

  @override
  State<MainScreen> createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  int _navIndex = 0;
  List<Widget> screens = [
    HomeScreen(),
    ChangeNotifierProvider(
        create: (BuildContext context) => ReserveProvider(ApiService(Dio())),
        child: ReservationsScreen())
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      bottomNavigationBar: MyCache.getString(key: "role") != "Staff"
          ? BottomNavigationBar(
              items: [
                BottomNavigationBarItem(icon: Icon(Icons.home), label: "Home"),
                BottomNavigationBarItem(
                    icon: Icon(Icons.favorite_border), label: "Reservations"),
                BottomNavigationBarItem(
                    icon: Icon(Icons.percent), label: "Offer"),
                BottomNavigationBarItem(
                    icon: Icon(Icons.person_2_outlined), label: "Profile"),
              ],
              backgroundColor: Colors.black,
              type: BottomNavigationBarType.fixed,
              currentIndex: _navIndex,
              selectedItemColor: Color(0xffBE7C01),
              unselectedItemColor: Colors.white,
              onTap: (int n) {
                setState(() {
                  _navIndex = n;
                });
              },
            )
          : null,
      body: screens[_navIndex],
    );
  }
}
