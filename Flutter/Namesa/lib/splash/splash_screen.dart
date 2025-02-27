// ignore_for_file: use_build_context_synchronously

import 'package:flutter/material.dart';

import '../login/views/screens/login_Screen.dart';

class SplashScreen extends StatefulWidget {
  const SplashScreen({super.key});

  @override
  State<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {
  @override
  void initState() {
    super.initState();
    Future.delayed(
        const Duration(seconds: 3),
        () => Navigator.of(context).pushReplacement(
            MaterialPageRoute(builder: (_) => const LoginScreen())));
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: Colors.black,
        body: Center(
            child: TweenAnimationBuilder(
                tween: Tween<double>(begin: 0, end: 1),
                duration: const Duration(milliseconds: 2700),
                builder: (context, val, child) => Opacity(
                    opacity: val <= 0.5 ? val : 1 - val,
                    child: Image.asset("assets/images/NamesaLogo.png")))));
  }
}
