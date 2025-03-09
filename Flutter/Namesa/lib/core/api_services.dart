import 'dart:io';

import 'package:dio/dio.dart';
import 'package:dio/io.dart';
import 'package:namesa/core/cached_data.dart';

class ApiService {
  final _baseUrl = 'https://10.0.2.2:7245/api/Account/';
  final _baseRoomUrl = 'https://10.0.2.2:7245/api/Room/';
  final _baseReserveUrl = 'https://10.0.2.2:7245/api/User/';

  final Dio _dio;

  ApiService(this._dio) {
    (_dio.httpClientAdapter as DefaultHttpClientAdapter).onHttpClientCreate =
        (HttpClient client) {
      client.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      return client;
    };
  }

  Future<Map<String, dynamic>> login(
      {required String username, required String password}) async {
    try {
      var response = await _dio.post('${_baseUrl}login',
          data: {"email": username, "password": password});
      return response.data;
    } catch (e) {
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> register(
      {required String username,
      required String email,
      required String address,
      required String password,
      required String userType,
      required String employementType,
      required String gender}) async {
    try {
      var response = await _dio.post('${_baseUrl}register', data: {
        "username": username,
        "email": email,
        "address": address,
        "password": password,
        "userType": userType,
        "employementType": employementType,
        "gender": gender
      }, options: Options(validateStatus: (status) {
        return status == 500; // Accept responses below 500
      }));
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> createRoom(
      {required String roomType,
      required double price,
      required int numOfBeds,
      required bool isSea}) async {
    try {
      var response = await _dio.post('${_baseRoomUrl}CreateRoom',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          data: {
            "roomType": roomType,
            "price": price,
            "numberOfBeds": numOfBeds,
            "isSea": isSea
          });
      // print(response.data!);
      return response.data;
    } catch (e) {
      // print(e);
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> updateRoom({
    required int roomId,
    required String roomType,
    bool isAvailable = true,
    required double price,
    required int numOfBeds,
    required bool isSea,
    int numberOfReviwers = 0,
    int rate = 0,
  }) async {
    try {
      var response = await _dio.put('${_baseRoomUrl}UpdateRoom',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          data: {
            "id": roomId,
            "roomType": roomType,
            "isAvaliable": isAvailable,
            "price": price,
            "numberOfBeds": numOfBeds,
            "isSea": isSea,
            "numberOfReviewers": numberOfReviwers,
            "rate": rate
          });
      return response.data;
    } catch (e) {
      // print(e);
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> deleteRoom(int roomId) async {
    try {
      var response = await _dio.delete('${_baseRoomUrl}DeleteRoom',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          queryParameters: {"roomId": roomId});
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> viewRoomDetails(int roomId) async {
    try {
      var response = await _dio.get('${_baseRoomUrl}ViewRoomDetails',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          queryParameters: {"roomId": roomId});
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> viewAllRooms(int pageSize, int pageIndex) async {
    try {
      var response = await _dio.get('${_baseRoomUrl}ViewAllRooms',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          queryParameters: {"pageSize": pageSize, "pageIndex": pageIndex});
      // print("From api service");
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> reserveRoom(
      int roomId, String checkIn, String checkOut, int paymentMethod) async {
    try {
      // print("From api service");
      var response = await _dio.post('${_baseReserveUrl}Reserve',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          data: {
            "RoomId": roomId,
            "From": checkIn,
            "To": checkOut,
            "PaymentMethod": paymentMethod
          });
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> getReservations() async {
    try {
      var response = await _dio.get(
        '${_baseReserveUrl}GetAllMyReservations',
        options: Options(headers: {
          "Authorization": "Bearer ${MyCache.getString(key: "token")}"
        }),
      );
      // print("From api service");
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> getReservationDetails(int reservationId) async {
    try {
      var response = await _dio.get('${_baseReserveUrl}GetReservationDetails',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          queryParameters: {"reservationId": reservationId});
      // print("From api service");
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> cancelReservation(int reservationId) async {
    try {
      var response = await _dio.delete('${_baseReserveUrl}CancelReservation',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          queryParameters: {"reservationId": reservationId});
      // print("From api service");
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      // print(e.toString());
      return {};
    }
  }

  Future<Map<String, dynamic>> updateReservation(int reservationId,
      String checkIn, String checkOut, int paymentMethod) async {
    try {
      var response = await _dio.put('${_baseReserveUrl}UpdateReservation',
          options: Options(headers: {
            "Authorization": "Bearer ${MyCache.getString(key: "token")}"
          }),
          data: {
            "ReservationId": reservationId,
            "From": checkIn,
            "To": checkOut,
            "PaymentMethod": paymentMethod
          });
      // print("From api service");
      // print(response.data);
      return response.data;
    } catch (e) {
      // print(e.toString());
      // print(e.toString());
      return {};
    }
  }
}
