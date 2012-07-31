﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.HTTP

{
    interface IUrlBuilder
    {        
        Credential Credential { get; set; }
        HttpPayload LoginPayload();
        HttpPayload LogoutPayload();
        HttpPayload DevicesPayload();
        HttpPayload DeviceDetailsPayload(int DeviceID);
        HttpPayload DeviceCommandsPayload(int DeviceID);
        HttpPayload DeviceCommandPayload(int DeviceID, string Name, int arg, string type);
        HttpPayload DeviceValuesPayload(int DeviceID);
        HttpPayload ScenesPayload();
        HttpPayload ScenesChangeNamePayload(int SceneID, string Name);
        HttpPayload StartScenePayload(int SceneID);

        HttpPayload GroupsPayload();
        HttpPayload GroupDetailsPayload(int GroupID);

        HttpPayload CommandsPayload();
        HttpPayload SendCommandsPayload(Models.BuiltinCommand Command);
    }
}
