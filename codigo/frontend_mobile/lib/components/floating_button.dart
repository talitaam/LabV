import 'package:flutter/material.dart';

class FloatingButton extends StatelessWidget {
  final String title;
  final Function onPressed;

  const FloatingButton(this.title, this.onPressed);

  @override
  Widget build(BuildContext context) {
    return Container(
        child: FloatingActionButton.extended(
      backgroundColor: const Color(0xFFF57F17),
      foregroundColor: Colors.black,
      onPressed: () {
        onPressed();
      },
      label: Text(title),
      icon: Icon(Icons.navigate_next),
    ));
  }
}
