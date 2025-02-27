import 'package:flutter/material.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:namesa/core/api_services.dart';
import 'package:namesa/core/cached_data.dart';

class LoginProvider extends ChangeNotifier {
  final ApiService apiService;

  LoginProvider(this.apiService);

  bool _isLoading = false;

  bool get isLoading => _isLoading;

  String? _errorMessage;

  String? get errorMessage => _errorMessage;

  Future<void> login(String username, String password) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    var data = await apiService.login(username: username, password: password);
    if (data.isNotEmpty) {
      _isLoading = false;
      _errorMessage = null;
      // print(data["token"]);
      MyCache.setString(key: "token", value: data["token"]);
      final Map<String, dynamic> decodedToken =
          JwtDecoder.decode(MyCache.getString(key: 'token'));
      final Map<String, String> filteredToken = {
        "email": decodedToken[
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] ??
            "N/A",
        "name": decodedToken[
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"] ??
            "N/A",
        "role": decodedToken[
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ??
            "N/A",
      };
      MyCache.setString(key: "role", value: filteredToken["role"]!);

      notifyListeners();
    } else {
      _isLoading = false;
      _errorMessage = "Try Again Please";
      notifyListeners();
    }
  }
}
