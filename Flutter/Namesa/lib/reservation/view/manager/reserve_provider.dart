import 'package:flutter/material.dart';
import 'package:namesa/core/api_services.dart';
import 'package:namesa/core/cached_data.dart';
import 'package:namesa/reservation/models/reservation_model.dart';

class ReserveProvider extends ChangeNotifier {
  final ApiService apiService;

  ReserveProvider(this.apiService);

  bool _isLoading = false;

  bool get isLoading => _isLoading;

  String? _errorMessage;

  String? get errorMessage => _errorMessage;
  var result;
  List<dynamic> reservations = [];
  String message = '';

  Future<void> reserveRoom(
      int roomId, String check_in, String check_out, int payment_method) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    var data = await apiService.reserveRoom(
        roomId, check_in, check_out, payment_method);

    result = ReservationResponse.fromJson(data);
    // print(data['data'] == null);
    if (data['data'] != null) {
      _isLoading = false;
      _errorMessage = null;
      MyCache.setString(key: 'token', value: data['data']['token']);
      // print(MyCache.getString(key: 'token'));
      notifyListeners();
    } else {
      _isLoading = false;
      _errorMessage = data['message'];
      // print("meesage");
      // print(_errorMessage);
      notifyListeners();
    }
  }

  Future<void> getAllReservations() async {
    try {
      _isLoading = true;
      notifyListeners();

      final response = await apiService.getReservations();

      if (response['statusCode'] == 200) {
        reservations = response['data'] ?? []; // Ensure empty list if no data
      } else {
        reservations = [];
      }
    } catch (e) {
      reservations = []; // Ensure list resets on error
    } finally {
      _isLoading = false;
      notifyListeners();
    }
  }

  Future<void> getReservationDetails(int reservationId) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    var data = await apiService.getReservationDetails(reservationId);

    // print(data);
    if (data.isNotEmpty) {
      _isLoading = false;
      _errorMessage = null;
      result = ReservationResponse.fromJson(data);
      // print(data['data']);
      // print(result.data!.guestName);
      // print("reservations");
      notifyListeners();
    } else {
      _isLoading = false;
      _errorMessage = data['message'];
      notifyListeners();
    }
  }

  Future<void> cancelReservation(int reservationId) async {
    try {
      _isLoading = true;
      notifyListeners();

      final response = await apiService.cancelReservation(reservationId);

      if (response['statusCode'] == 200) {
        reservations.removeWhere(
            (reservation) => reservation['reservationId'] == reservationId);

        // Force UI update when the last item is removed
        if (reservations.isEmpty) {
          reservations.clear(); // Explicitly clear
        }

        message = response['message'];
      }
    } catch (e) {
      message = "Failed to cancel reservation";
    } finally {
      _isLoading = false;
      notifyListeners(); // Make sure UI rebuilds
    }
  }

  Future<void> updateReservation(int reservationId, String check_in,
      String check_out, int payment_method) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    var data = await apiService.updateReservation(
        reservationId, check_in, check_out, payment_method);

    print(data);
    if (data.isNotEmpty) {
      _isLoading = false;
      _errorMessage = null;
      print("message");
      print(data['message']);
      print(message);
      message = data['message'];
      // result = ReservationResponse.fromJson(data);
      // print(result.data!.guestName);
      // print("reservations");
      notifyListeners();
    } else {
      _isLoading = false;
      _errorMessage = data['message'];
      message = data['message'];
      notifyListeners();
    }
    notifyListeners();
  }
}
