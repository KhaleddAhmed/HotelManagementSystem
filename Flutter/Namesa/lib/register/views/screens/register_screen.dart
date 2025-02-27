import 'package:dio/dio.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:namesa/core/api_services.dart';
import 'package:namesa/login/views/screens/login_Screen.dart';
import 'package:namesa/register/views/manager/register_provider.dart';
import 'package:provider/provider.dart';

import '../../../shared widgets/logo.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  String? _isUser;
  String? _employeeRole;
  String? _gender;
  bool _done = true;

  final TextEditingController _usernameCntr = TextEditingController();
  final TextEditingController _emailCntr = TextEditingController();
  final TextEditingController _addressCntr = TextEditingController();
  final TextEditingController _passwordCntr = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.black,
      body: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Image.asset("assets/images/NamesaLogo.png", scale: 16 / 9),
            Padding(
              padding: EdgeInsets.symmetric(horizontal: 20.w),
              child: ChangeNotifierProvider(
                create: (context) => RegisterProvider(ApiService(Dio())),
                child: Consumer<RegisterProvider>(
                  builder: (context, register, child) => register.isLoading
                      ? const Center(child: CircularProgressIndicator())
                      : Container(
                          padding: EdgeInsets.all(20.sp),
                          decoration: BoxDecoration(
                              color: Colors.white.withOpacity(0.5),
                              borderRadius: BorderRadius.circular(4)),
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            children: [
                              const Logo("Register"),
                              SizedBox(
                                height: 30.h,
                              ),
                              TextFormField(
                                  controller: _usernameCntr,
                                  cursorColor: Colors.black,
                                  onChanged: (s) {
                                    setState(() {});
                                  },
                                  decoration: InputDecoration(
                                      helperText:
                                          "Username must be more than 5 and unique",
                                      helperStyle: TextStyle(
                                          color: _usernameCntr.text.isEmpty ||
                                                  _usernameCntr.text.length < 4
                                              ? const Color(0xffD10000)
                                              : const Color(0xff007C04),
                                          fontWeight: FontWeight.bold),
                                      labelText: "Username",
                                      labelStyle:
                                          const TextStyle(color: Colors.black),
                                      enabledBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedErrorBorder:
                                          const OutlineInputBorder(
                                              borderSide: BorderSide(
                                                  color: Colors.red, width: 2)),
                                      errorBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.red, width: 2)))),
                              SizedBox(
                                height: 20.h,
                              ),
                              TextFormField(
                                  controller: _emailCntr,
                                  cursorColor: Colors.black,
                                  onChanged: (s) {
                                    setState(() {});
                                  },
                                  decoration: InputDecoration(
                                      helperText:
                                          "Email must be valid and unique",
                                      helperStyle: TextStyle(
                                          color: RegExp(
                                            r"^[a-zA-Z0-9._%+-]{1,20}@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                                          ).hasMatch(_emailCntr.text)
                                              ? const Color(0xff007C04)
                                              : const Color(0xffD10000),
                                          fontWeight: FontWeight.bold),
                                      labelText: "Email",
                                      labelStyle:
                                          const TextStyle(color: Colors.black),
                                      enabledBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedErrorBorder:
                                          const OutlineInputBorder(
                                              borderSide: BorderSide(
                                                  color: Colors.red, width: 2)),
                                      errorBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.red, width: 2)))),
                              SizedBox(
                                height: 20.h,
                              ),
                              TextFormField(
                                  controller: _addressCntr,
                                  cursorColor: Colors.black,
                                  onChanged: (s) {
                                    setState(() {});
                                  },
                                  decoration: InputDecoration(
                                      helperText: "Address must be valid",
                                      helperStyle: TextStyle(
                                          color: _addressCntr.text.isEmpty ||
                                                  _addressCntr.text.length < 3
                                              ? const Color(0xffD10000)
                                              : const Color(0xff007C04),
                                          fontWeight: FontWeight.bold),
                                      labelText: "Address",
                                      labelStyle:
                                          const TextStyle(color: Colors.black),
                                      enabledBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedErrorBorder:
                                          const OutlineInputBorder(
                                              borderSide: BorderSide(
                                                  color: Colors.red, width: 2)),
                                      errorBorder: const OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.red, width: 2)))),
                              SizedBox(
                                height: 20.h,
                              ),
                              TextFormField(
                                  obscureText: true,
                                  controller: _passwordCntr,
                                  cursorColor: Colors.black,
                                  onChanged: (s) {
                                    setState(() {});
                                  },
                                  decoration: const InputDecoration(
                                      labelText: "Password",
                                      labelStyle:
                                          TextStyle(color: Colors.black),
                                      enabledBorder: OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedBorder: OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.black, width: 2)),
                                      focusedErrorBorder: OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.red, width: 2)),
                                      errorBorder: OutlineInputBorder(
                                          borderSide: BorderSide(
                                              color: Colors.red, width: 2)))),
                              SizedBox(
                                height: 5.h,
                              ),
                              Text(
                                  "Password must contain at least capital letter, non-numeric characters and numbers and more than 5 characters in total",
                                  style: TextStyle(
                                    color: RegExp(
                                              r"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{5,}$",
                                            ).hasMatch(_passwordCntr.text) &&
                                            _passwordCntr.text.length > 5
                                        ? const Color(0xff007C04)
                                        : const Color(0xffD10000),
                                    fontSize: 12.sp,
                                    fontWeight: FontWeight.w600,
                                  )),
                              SizedBox(
                                height: 20.h,
                              ),
                              Container(
                                padding: EdgeInsets.symmetric(horizontal: 12.w),
                                decoration: BoxDecoration(
                                    borderRadius: BorderRadius.circular(4),
                                    border: Border.all(
                                        color: Colors.black, width: 2)),
                                child: DropdownButtonHideUnderline(
                                  child: DropdownButton(
                                    isExpanded: true,
                                    borderRadius: BorderRadius.circular(4),
                                    items: const [
                                      DropdownMenuItem<String>(
                                        value: "User",
                                        child: Text("User"),
                                      ),
                                      DropdownMenuItem<String>(
                                        value: "Staff",
                                        child: Text("Staff"),
                                      ),
                                    ],
                                    hint: const Text(
                                      "User Type",
                                      style: TextStyle(color: Colors.black),
                                    ),
                                    iconEnabledColor: Colors.black,
                                    value: _isUser,
                                    onChanged: (val) {
                                      setState(() {
                                        _isUser = val!;
                                      });
                                    },
                                  ),
                                ),
                              ),
                              Text("Type must be chosen",
                                  style: TextStyle(
                                    color: _isUser != null
                                        ? const Color(0xff007C04)
                                        : const Color(0xffD10000),
                                    fontSize: 12.sp,
                                    fontWeight: FontWeight.w600,
                                  )),
                              SizedBox(
                                height: 20.h,
                              ),
                              _isUser == "Staff"
                                  ? Column(
                                      mainAxisSize: MainAxisSize.min,
                                      children: [
                                        Container(
                                          padding: EdgeInsets.symmetric(
                                              horizontal: 12.w),
                                          decoration: BoxDecoration(
                                              borderRadius:
                                                  BorderRadius.circular(4),
                                              border: Border.all(
                                                  color: Colors.black,
                                                  width: 2)),
                                          child: DropdownButtonHideUnderline(
                                            child: DropdownButton(
                                              isExpanded: true,
                                              borderRadius:
                                                  BorderRadius.circular(4),
                                              items: const [
                                                DropdownMenuItem<String>(
                                                  value: "Manager",
                                                  child: Text("Manager"),
                                                ),
                                                DropdownMenuItem<String>(
                                                  value: "Marketing",
                                                  child: Text("Marketing"),
                                                ),
                                                DropdownMenuItem<String>(
                                                  value: "Employee",
                                                  child: Text("Employee"),
                                                ),
                                              ],
                                              hint: const Text(
                                                "Employment Type",
                                                style: TextStyle(
                                                    color: Colors.black),
                                              ),
                                              iconEnabledColor: Colors.black,
                                              value: _employeeRole,
                                              onChanged: (val) {
                                                setState(() {
                                                  _employeeRole = val!;
                                                });
                                              },
                                            ),
                                          ),
                                        ),
                                        Text("Type must be chosen",
                                            style: TextStyle(
                                              color: _employeeRole != null
                                                  ? const Color(0xff007C04)
                                                  : const Color(0xffD10000),
                                              fontSize: 12.sp,
                                              fontWeight: FontWeight.w600,
                                            )),
                                      ],
                                    )
                                  : Container(),
                              SizedBox(
                                height: 20.h,
                              ),
                              Flex(
                                direction: Axis.horizontal,
                                children: [
                                  Expanded(
                                    flex: 1,
                                    child: Radio(
                                        focusColor: Colors.black,
                                        activeColor: Colors.black,
                                        value: "Male",
                                        groupValue: _gender,
                                        onChanged: (val) {
                                          setState(() {
                                            _gender = "Male";
                                          });
                                        }),
                                  ),
                                  Expanded(
                                      flex: 2,
                                      child: Text(
                                        "Male",
                                        style: TextStyle(
                                            color: Colors.black,
                                            fontSize: 15.sp),
                                      )),
                                  Expanded(
                                    flex: 1,
                                    child: Radio(
                                        value: "Female",
                                        focusColor: Colors.black,
                                        activeColor: Colors.black,
                                        groupValue: _gender,
                                        onChanged: (val) {
                                          setState(() {
                                            _gender = "Female";
                                          });
                                        }),
                                  ),
                                  Expanded(
                                      flex: 2,
                                      child: Text(
                                        "Female",
                                        style: TextStyle(
                                            color: Colors.black,
                                            fontSize: 15.sp),
                                      )),
                                ],
                              ),
                              Text("Gender must be chosen",
                                  style: TextStyle(
                                    color: _gender != null
                                        ? const Color(0xff007C04)
                                        : const Color(0xffD10000),
                                    fontSize: 12.sp,
                                    fontWeight: FontWeight.w600,
                                  )),
                              SizedBox(
                                height: 20.h,
                              ),
                              RichText(
                                  text: TextSpan(children: [
                                TextSpan(
                                    text: "Have an account? ",
                                    style: TextStyle(
                                        color: Colors.black, fontSize: 16.sp)),
                                TextSpan(
                                    text: "Login",
                                    style: TextStyle(
                                        color: Colors.blue, fontSize: 16.sp),
                                    recognizer: TapGestureRecognizer()
                                      ..onTap = () {
                                        Navigator.of(context).pushReplacement(
                                            MaterialPageRoute(
                                                builder: (context) =>
                                                    const LoginScreen()));
                                      })
                              ])),
                              SizedBox(
                                height: 20.h,
                              ),
                              ElevatedButton(
                                  onPressed: () async {
                                    if ((_usernameCntr.text.isNotEmpty &&
                                            _usernameCntr.text.length >= 4) &&
                                        (RegExp(
                                          r"^[a-zA-Z0-9._%+-]{1,20}@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                                        ).hasMatch(_emailCntr.text)) &&
                                        (_addressCntr.text.isNotEmpty &&
                                            _addressCntr.text.length >= 3) &&
                                        (RegExp(
                                              r"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{5,}$",
                                            ).hasMatch(_passwordCntr.text) &&
                                            _passwordCntr.text.length >= 5) &&
                                        (_isUser != null) &&
                                        (_employeeRole != null) &&
                                        (_gender != null)) {
                                      setState(() {
                                        _done = true;
                                      });
                                      await register.register(
                                          _usernameCntr.text,
                                          _emailCntr.text,
                                          _addressCntr.text,
                                          _passwordCntr.text,
                                          _isUser!,
                                          _employeeRole ?? "",
                                          _gender!);
                                      Navigator.of(context).pushReplacement(
                                          MaterialPageRoute(
                                              builder: (context) =>
                                                  const LoginScreen()));
                                    } else {
                                      setState(() {
                                        _done = false;
                                      });
                                    }
                                  },
                                  style: ElevatedButton.styleFrom(
                                      shape: RoundedRectangleBorder(
                                          borderRadius:
                                              BorderRadius.circular(4)),
                                      backgroundColor: const Color(0xff7C6A46),
                                      padding: EdgeInsets.symmetric(
                                          horizontal: 25.w, vertical: 10.h)),
                                  child: Text(
                                    "Register",
                                    style: TextStyle(
                                        color: Colors.white, fontSize: 16.sp),
                                  )),
                              SizedBox(
                                height: 20.h,
                              ),
                              !_done
                                  ? Text(
                                      "Make Sure every input is correct",
                                      style: TextStyle(
                                          color: Colors.red, fontSize: 16.sp),
                                    )
                                  : Container()
                            ],
                          ),
                        ),
                ),
              ),
            ),
            SizedBox(
              height: 20.h,
            )
          ],
        ),
      ),
    );
  }
}
