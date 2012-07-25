zVirtualScenes-Client
=====================

A cross platform client SDK Implementation for zVirtualScenes (http://code.google.com/p/zvirtualscenes/)
Right now we support Windows Phone 7 and Desktop Apps, and will look to also support Mono.

Includes a simple command line interface (vcmd) for executing commands using this generic library to your sever.

vcmd usage:

/Password:<string>  Password to the zVirtualScenes Server (short form /pa)
/Host:<string>      Host to the zVirtualScenes Serer (http://server.com) (short form /h)
/Port:<int>         Port to the zVirtualScenes Serer (short form /po)
/Action:{}          Action to take (short form /a)
/DeviceID:<int>     Device to take the action on (short form /d)
/SceneID:<int>      Scene to take the action on (short form /s)
/GroupID:<int>      Group to take the action on (short form /g)
/Name:<string>      Name parameter (short form /n)
/arg:<int>          Argument to pass
/type:<string>      Type of command (short form /t)
@<file>             Read response file for more options

Examples:

Start A Scene:

vcmd /host:HOST /Password:PASSWORD /Port:PORT /Action:StartScene /SceneID:1

List Devices:

vcmd /host:HOST /Password:PASSWORD /Port:PORT /Action:ListDevices


Device Command (Set Device to 0) aka Turn this device off if it is a switch:

vcmd /host:HOST /Password:PASSWORD /Port:PORT /Action:DeviceCommand /DeviceID:1 /name:DYNAMIC_CMD_BASIC /arg:0 /type:device



Device Command (Set Device to 255) aka Turn this device ON if it is a switch:

vcmd /host:HOST /Password:PASSWORD /Port:PORT /Action:DeviceCommand /DeviceID:1 /name:DYNAMIC_CMD_BASIC /arg:255 /type:device


