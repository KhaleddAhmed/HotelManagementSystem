class ReservationResponse {
  int? statusCode;
  String? message;
  Data? data;

  ReservationResponse({this.statusCode, this.message, this.data});

  ReservationResponse.fromJson(Map<String, dynamic> json) {
    statusCode = json['statusCode'];
    message = json['message'];
    data = json['data'] != null ? new Data.fromJson(json['data']) : null;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['statusCode'] = this.statusCode;
    data['message'] = this.message;
    if (this.data != null) {
      data['data'] = this.data!.toJson();
    }
    return data;
  }
}

class Data {
  String? guestName;
  String? from;
  String? to;
  int? totalNumberOfDays;
  double? totalPrice;
  int? paymentMethod;
  int? reservationStatus;

  Data(
      {this.guestName,
      this.from,
      this.to,
      this.totalNumberOfDays,
      this.totalPrice,
      this.paymentMethod,
      this.reservationStatus});

  Data.fromJson(Map<String, dynamic> json) {
    guestName = json['guestName'];
    from = json['from'];
    to = json['to'];
    totalNumberOfDays = json['totalNumberOfDays'];
    totalPrice = json['totalPrice'];
    paymentMethod = json['paymentMethod'];
    reservationStatus = json['reservationStatus'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['guestName'] = this.guestName;
    data['from'] = this.from;
    data['to'] = this.to;
    data['totalNumberOfDays'] = this.totalNumberOfDays;
    data['totalPrice'] = this.totalPrice;
    data['paymentMethod'] = this.paymentMethod;
    data['reservationStatus'] = this.reservationStatus;
    return data;
  }
}
