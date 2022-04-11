import 'package:flutter/material.dart';

class ValidationError extends StatelessWidget {
  final bool isValid;
  final String errorMessage;

  const ValidationError(this.isValid, this.errorMessage);

  @override
  Widget build(BuildContext context) {
    return isValid == false
        ? Padding(
            padding: EdgeInsets.fromLTRB(0.0, 10.0, 0.0, 15.0),
            child: Text(
              errorMessage,
              style: TextStyle(color: Color(0xFFe53935), fontSize: 12),
            ),
          )
        : Text('');
  }
}
