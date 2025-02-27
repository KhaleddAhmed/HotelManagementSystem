import 'package:flutter/material.dart';
import 'package:namesa/core/api_services.dart';

class RoomProvider extends ChangeNotifier {
  final ApiService apiService;

  RoomProvider(this.apiService);

  bool _isLoading = false;

  bool get isLoading => _isLoading;

  String? _errorMessage;

  String? get errorMessage => _errorMessage;

  Future<void> createRoom(
      String roomType, double price, int numOfBeds, bool isSea) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    var data = await apiService.createRoom(
      roomType: roomType,
      price: price,
      numOfBeds: numOfBeds,
      isSea: isSea,
    );
    if (data.isNotEmpty) {
      _isLoading = false;
      _errorMessage = null;

      // print(data);

      notifyListeners();
    } else {
      _isLoading = false;
      _errorMessage = "Try Again Please";
      notifyListeners();
    }
  }

  Future<void> updateRoom(int roomId, String roomType, double price,
      int numOfBeds, bool isSea) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    var data = await apiService.updateRoom(
      roomType: roomType,
      price: price,
      numOfBeds: numOfBeds,
      isSea: isSea,
      roomId: roomId,
    );
    if (data.isNotEmpty) {
      _isLoading = false;
      _errorMessage = null;

      // print(data);

      notifyListeners();
    } else {
      _isLoading = false;
      _errorMessage = "Try Again Please";
      notifyListeners();
    }
  }

  Future<void> deleteRoom(int roomId) async {
    _isLoading = true;
    _errorMessage = null;
    notifyListeners();

    var data = await apiService.deleteRoom(roomId = roomId);
    if (data.isNotEmpty) {
      _isLoading = false;
      _errorMessage = null;

      // print(data);

      notifyListeners();
    } else {
      _isLoading = false;
      _errorMessage = "Try Again Please";
      // print("failed");
      notifyListeners();
    }
  }
}
