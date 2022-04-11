// ignore_for_file: non_constant_identifier_names

import 'package:date_format/date_format.dart';
import 'package:mask_text_input_formatter/mask_text_input_formatter.dart';

final DATE_MASK_FORMATTER = new MaskTextInputFormatter(
    mask: '##/##/####', filter: {"#": RegExp(r'[0-9]')});

final TIME_MASK_FORMATTER =
    new MaskTextInputFormatter(mask: '##:##', filter: {"#": RegExp(r'[0-9]')});

stringToDate(date) {
  if (date == null || date == '') {
    return null;
  }

  DateTime stringToDate = DateTime.parse(date);
  return formatDate(
    stringToDate,
    [dd, '/', mm, '/', yyyy],
  );
}

stringToDateString(date) {
  if (date == null || date == '') {
    return null;
  }
  var arrDate = date.split('/');

  var year = int.parse(arrDate[2]);
  var month = int.parse(arrDate[1]);
  var day = int.parse(arrDate[0]);

  var dateFormatted = new DateTime.utc(year, month, day).toString();

  return dateFormatted
      .substring(0, dateFormatted.length - 5)
      .replaceAll(' ', "T");
}
