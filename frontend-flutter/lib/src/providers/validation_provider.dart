import 'package:people_mobile/src/models/form-field_model.dart';

class ValidationProvider {
  List<String> arrayErrors = [];

  setFormField(FormField formField, dynamic value) {
    switch (formField.datatype) {
      case 'text':
        stringValidation(formField, value);
        break;
      case 'number':
        numberValidation(formField, value);
        break;
      case 'decimal number':
        decimalValidation(formField, value);
        break;
      case 'date':
        dateValidation(formField, value);
        break;
      case 'note':
        stringValidation(formField, value);
        break;
      default:
        break;
    }
  }

  stringValidation(FormField formField, String value) {
    if (value == '')
      arrayErrors.add(formField.name);
    else
      switch (formField.constraints) {
        case 'Alphanumeric':
          if (!alphanumericIsvalid(value)) {
            arrayErrors.add(formField.name);
          }
          break;
        case 'Alphabetic':
          if (!alphabeticIsvalid(value)) arrayErrors.add(formField.name);
          break;
        case 'Email':
          if (!emailIsvalid(value)) arrayErrors.add(formField.name);
          break;
        case 'Numbers':
          if (!numbersIsvalid(value)) arrayErrors.add(formField.name);
          break;
        default:
          break;
      }
  }

  numberValidation(FormField formField, String value) {
    if (value == '')
      arrayErrors.add(formField.name);
    else
      switch (formField.constraints) {
        case 'Number > 0':
          if (!integerGreaterThanZero(value)) arrayErrors.add(formField.name);
          break;
        case 'Number >= 0':
          if (!integerEqualGreaterThanZero(value)) {
            arrayErrors.add(formField.name);
          }
          break;
        case 'Number < 0':
          if (!integerLessThanZero(value)) arrayErrors.add(formField.name);
          break;
        default:
          break;
      }
  }

  decimalValidation(FormField formField, String value) {
    if (value == '')
      arrayErrors.add(formField.name);
    else
      switch (formField.constraints) {
        case 'Number > 0':
          if (!decimalGreaterThanZero(value)) arrayErrors.add(formField.name);
          break;
        case 'Number >= 0':
          if (!decimalEqualGreaterThanZero(value))
            arrayErrors.add(formField.name);
          break;
        case 'Number < 0':
          if (!decimalLessThanZero(value)) arrayErrors.add(formField.name);
          break;
        default:
          break;
      }
  }

  dateValidation(FormField formField, String value) {
    if (value == '')
      arrayErrors.add(formField.name);
    else
      switch (formField.constraints) {
        case 'From today':
          if (!fromToday(value)) arrayErrors.add(formField.name);
          break;
        case 'Until today':
          if (!untilToday(value)) arrayErrors.add(formField.name);
          break;
        default:
          break;
      }
  }

  bool alphanumericIsvalid(String str) {
    final regexp = new RegExp(r'^[A-Za-zä-üÄ-Üá-úÁ-Ú0-9 ]+$');
    return regexp.hasMatch(str) ? true : false;
  }

  bool alphabeticIsvalid(String string) {
    final regexp = new RegExp(r'^[A-Za-zä-üÄ-Üá-úÁ-Ú ]+$');
    return regexp.hasMatch(string) ? true : false;
  }

  bool emailIsvalid(String email) {
    final regexp = new RegExp(r'\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b');
    return regexp.hasMatch(email) ? true : false;
  }

  bool numbersIsvalid(String number) {
    final regexp = new RegExp(r'(^\d{1,10}$)');
    return regexp.hasMatch(number) ? true : false;
  }

  bool integerGreaterThanZero(String number) {
    if (int.tryParse(number) != null)
      return int.tryParse(number) > 0 ? true : false;
    else
      return false;
  }

  bool integerEqualGreaterThanZero(String number) {
    if (int.tryParse(number) != null)
      return int.tryParse(number) >= 0 ? true : false;
    else
      return false;
  }

  bool integerLessThanZero(String number) {
    if (int.tryParse(number) != null)
      return int.tryParse(number) < 0 ? true : false;
    else
      return false;
  }

  bool decimalGreaterThanZero(String number) {
    if (double.tryParse(number) != null)
      return double.tryParse(number) > 0 ? true : false;
    else
      return false;
  }

  bool decimalEqualGreaterThanZero(String number) {
    if (double.tryParse(number) != null)
      return double.tryParse(number) >= 0 ? true : false;
    else
      return false;
  }

  bool decimalLessThanZero(String number) {
    if (double.tryParse(number) != null)
      return double.tryParse(number) < 0 ? true : false;
    else
      return false;
  }

  bool fromToday(String _date) {
    var today = new DateTime.now();
    var date = DateTime.parse(_date);
    return date.isAfter(today.add(Duration(days: -1))) ? true : false;
  }

  bool untilToday(String _date) {
    var today = new DateTime.now();
    var date = DateTime.parse(_date);
    return date.isBefore(today) ? true : false;
  }

  getBuildedMessage() {
    var msg = '';
    var verb = '';
    if (arrayErrors.length > 1) {
      msg = 'The fields ';
      verb = ' are';
    } else {
      msg = 'The field ';
      verb = ' is';
    }

    arrayErrors.forEach((e) {
      msg += e + ', ';
    });
    msg = msg.substring(0, msg.length - 2);
    msg += verb + ' invalid!!';
    arrayErrors = [];
    return msg;
  }
}
