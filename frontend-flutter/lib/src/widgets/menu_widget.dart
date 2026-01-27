import 'package:flutter/material.dart';

Drawer createMenu(BuildContext context) {
  return Drawer(
    child: ListView(
      padding: EdgeInsets.zero,
      children: <Widget>[
        DrawerHeader(
          child: Container(),
          decoration: BoxDecoration(
            image: DecorationImage(
                image: ExactAssetImage('assets/images/logo01.png'),
                fit: BoxFit.cover),
          ),
        ),
        ListTile(
          leading: Icon(Icons.pages, color: Theme.of(context).primaryColor),
          title: Text('DashBoard'),
          onTap: () {
            Navigator.pop(context);
            Navigator.pushReplacementNamed(context, 'dashboard');
          },
        ),
        ListTile(
          leading: Icon(Icons.people, color: Theme.of(context).primaryColor),
          title: Text('Staff'),
          onTap: () {
            Navigator.pop(context);
            Navigator.pushReplacementNamed(context, 'staff');
          },
        ),
        ListTile(
          leading: Icon(Icons.person, color: Theme.of(context).primaryColor),
          title: Text('Clients'),
          onTap: () {
            Navigator.pop(context);
            Navigator.pushReplacementNamed(context, 'clients');
          },
        ),
        ListTile(
          leading: Icon(Icons.person, color: Theme.of(context).primaryColor),
          title: Text('Programs'),
          onTap: () {
            Navigator.pop(context);
            Navigator.pushReplacementNamed(context, 'programs');
          },
        ),
        ListTile(
          leading:
              Icon(Icons.calendar_today, color: Theme.of(context).primaryColor),
          title: Text('Staff Schedule'),
          onTap: () {
            Navigator.pop(context);
            Navigator.pushReplacementNamed(context, 'staff-schedule');
          },
        ),
        ListTile(
          leading: Icon(Icons.map, color: Theme.of(context).primaryColor),
          title: Text('Geo Tracking'),
          onTap: () {
            Navigator.pop(context);
            Navigator.pushReplacementNamed(context, 'geotracking');
          },
        ),
        ListTile(
          leading: Icon(Icons.alarm, color: Theme.of(context).primaryColor),
          title: Text('Time Tracker'),
          onTap: () {
            Navigator.pop(context);
            Navigator.pushReplacementNamed(context, 'timetracker');
          },
        )
      ],
    ),
  );
}
