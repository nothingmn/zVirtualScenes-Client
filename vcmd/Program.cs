using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient;

namespace vcmd
{
    class Program
    {
        static Arguments a = new Arguments();
        static bool running = true;
        static Client client;
        static void Main(string[] args)
        {

            if (vcmd.Parser.ParseArgumentsWithUsage(args, a))
            {
                var configurationReader = new zVirtualClient.Configuration.AppConfigConfigurationReader();
                if (string.IsNullOrEmpty(a.Host))
                {
                    a.Host = configurationReader.ReadSetting<string>("Host");
                    if (string.IsNullOrEmpty(a.Host))
                    {
                        throw new ArgumentNullException("Host");
                    }
                }
                if (string.IsNullOrEmpty(a.Password))
                {
                    a.Password = configurationReader.ReadSetting<string>("Password");
                    if (string.IsNullOrEmpty(a.Password))
                    {
                        throw new ArgumentNullException("Password");
                    }
                }
                if (a.Port <= 0)
                {
                    a.Port = configurationReader.ReadSetting<int>("Port");
                    if ((a.Port <= 0))
                    {
                        throw new ArgumentNullException("Port");
                    }
                }
                //     insert application code here
                Credentials c = new Credentials(a.Host, a.Port, null, a.Password);
                client = new Client(c);
                client.OnError += new zVirtualClient.Interfaces.Error(client_OnError);
                client.OnLogin += new zVirtualClient.Interfaces.LoginResponse(client_OnLogin);
                client.OnLogout += new zVirtualClient.Interfaces.LogoutResponse(client_OnLogout);

                client.OnChangeSceneName += new zVirtualClient.Interfaces.SceneNameChangeResponse(client_OnChangeSceneName);
                client.OnCommands += new zVirtualClient.Interfaces.CommandsResponse(client_OnCommands);
                client.OnDeviceCommand += new zVirtualClient.Interfaces.DeviceCommandResponse(client_OnDeviceCommand);
                client.OnDeviceCommands += new zVirtualClient.Interfaces.DeviceCommandsResponse(client_OnDeviceCommands);
                client.OnDeviceDetails += new zVirtualClient.Interfaces.DeviceDetailsResponse(client_OnDeviceDetails);
                client.OnDevices += new zVirtualClient.Interfaces.DevicesResponse(client_OnDevices);
                client.OnDeviceValues += new zVirtualClient.Interfaces.DeviceValuesResponse(client_OnDeviceValues);
                client.OnGroupDetails += new zVirtualClient.Interfaces.GroupDetailsResponse(client_OnGroupDetails);
                client.OnGroups += new zVirtualClient.Interfaces.GroupsResponse(client_OnGroups);
                client.OnScenes += new zVirtualClient.Interfaces.SceneResponse(client_OnScenes);
                client.OnSendCommand += new zVirtualClient.Interfaces.CommandsResponse(client_OnSendCommand);
                client.OnStartScene += new zVirtualClient.Interfaces.SceneNameChangeResponse(client_OnStartScene);
                Console.WriteLine("Trying to login..");
                client.Login();
                while (running)
                {
                    System.Threading.Thread.Sleep(500);
                }

            }

        }
        static void Logout()
        {
            Console.WriteLine("All done, logging out");
            client.Logout();
        }
        static void TakeAction()
        {
            Console.WriteLine(string.Format("Action:{0}, DeviceID:{1}, SceneID:{2}, Name:{3}, arg:{4}, type:{5}", a.Action, a.DeviceID, a.SceneID, a.Name, a.arg, a.type));
            switch (a.Action)
            {
                case Actions.DeviceCommand:
                    client.DeviceCommand(a.DeviceID, a.Name, a.arg, a.type);
                    break;
                case Actions.ChangeSceneName:
                    client.ChangeSceneName(a.SceneID, a.Name);
                    break;
                case Actions.DeviceDetails:
                    client.DeviceDetails(a.DeviceID);
                    break;
                case Actions.ListBuildInCommands:
                    client.Commands();
                    break;
                case Actions.ListDeviceCommands:
                    client.DeviceCommands(a.DeviceID);
                    break;
                case Actions.ListDevices:
                    client.Devices();
                    break;
                case Actions.ListDeviceValues:
                    client.DeviceValues(a.DeviceID);
                    break;
                case Actions.ListGroups:
                    client.Groups();
                    break;
                case Actions.ListGroupsDetails:
                    client.GroupDetails(a.GroupID);
                    break;
                case Actions.ListScenes:
                    client.Scenes();
                    break;
                case Actions.SendBuildInCommand:                    
                    break;
                case Actions.StartScene:
                    client.StartScene(a.SceneID);
                    break;
                default:

                    break;
            }
        }
        static void client_OnStartScene(zVirtualClient.Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            if (SceneNameChangeResponse.success)
            {
                Console.WriteLine(string.Format("Scene start was successful, {0}", SceneNameChangeResponse.desc));
            }
            else
            {
                Console.WriteLine(string.Format("Scene start was not successful, {0}", SceneNameChangeResponse.desc));
            }
            Logout();
        }

        static void client_OnSendCommand(zVirtualClient.Models.CommandsResponse CommandsResponse)
        {            
            Logout();
            throw new NotImplementedException();
        }

        static void client_OnScenes(zVirtualClient.Models.SceneResponse SceneResponse)
        {
            if (SceneResponse.success)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var val in SceneResponse.scenes)
                {
                    sb.Append(string.Format("Scene Details:\nid:\t\t{1}\ncount:\t\t{0}\nis_running:\t\t{2}\nname:\t\t{3}\n", val.cmd_count, val.id, val.is_running, val.name));
                }
                Console.WriteLine(sb.ToString());
            }
            else
            {
                Console.WriteLine(string.Format("Scenes query was not successful"));
            }
            Logout();
        }

        static void client_OnLogout(zVirtualClient.Models.LoginResponse LoginResponse)
        {
            Console.WriteLine("Logout Result:" + LoginResponse.success);
            running = false;

        }

        static void client_OnGroups(zVirtualClient.Models.GroupsResponse GroupsResponse)
        {
            if (GroupsResponse.success)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var val in GroupsResponse.groups)
                {
                    sb.Append(string.Format("Group Details:\nid:\t\t{1}\ncount:\t\t{0}\nname:\t\t{2}\n",  val.count, val.id, val.name));
                    foreach (var i in val.devices)
                    {
                        sb.Append(string.Format("\nDevice Name\r\r{0}", i.name));
                    }
                }
                Console.WriteLine(sb.ToString());
            }
            else
            {
                Console.WriteLine(string.Format("Groups query was not successful"));
            }
            Logout();
        }

        static void client_OnGroupDetails(zVirtualClient.Models.GroupDetailsResponse GroupDetailsResponse)
        {
            if (GroupDetailsResponse.success)
            {
                System.Text.StringBuilder sb = new StringBuilder();

                sb.Append(string.Format("Group Details:\nid:\t\t{1}\ncount:\t\t{0}\nname:\t\t{2}\n",
                    GroupDetailsResponse.group.count,
                    GroupDetailsResponse.group.id,
                    GroupDetailsResponse.group.name
                    ));
                foreach (var item in GroupDetailsResponse.group.devices)
                {
                    sb.Append("Device Name:" + item.name);
                }

                Console.WriteLine(sb.ToString());
            }
            else
            {
                Console.WriteLine(string.Format("Group Details query was not successful"));
            }
            Logout();
        }

        static void client_OnDeviceValues(zVirtualClient.Models.DeviceValues DeviceValuesResponse)
        {
            if (DeviceValuesResponse.success)
            {
                foreach (var val in DeviceValuesResponse.values)
                {

                    Console.WriteLine( string.Format("Device Value Details:\nid:\t\t{1}\ngenre:\t\t{0}\nindex2:\t\t{2}\nlabel_name:\t\t{3}\nread_only:\t\t{4}\ntype:\t\t{5}\nvalue:\t\t{6}\nvalue_id:\t\t{7}\n", val.grene, val.id, val.index2, val.label_name, val.read_only, val.type, val.value, val.value_id ));
                }
            }
            else
            {
                Console.WriteLine(string.Format("Device Values query was not successful"));
            }
            Logout();
        }

        static void client_OnDevices(zVirtualClient.Models.Devices DevicesResponse)
        {
            if (DevicesResponse.success)
            {
                Console.WriteLine(string.Format("Devices Found:{0}", DevicesResponse.devices.Count));
                foreach (var item in DevicesResponse.devices)
                {
                    Console.WriteLine(string.Format("Device Details:\nid:\t\t{0}\nlevel:\t\t{1}\nlevel_txt:\t\t{2}\nname:\t\t{3}\non_off:\t\t{4}\nplugin_name:\t\t{5}\ntype:\t\t{6}\n", item.id, item.level, item.level_txt, item.name, item.on_off, item.plugin_name, item.type));                    
                }
            }
            else
            {
                Console.WriteLine(string.Format("List Devices query was not successful"));

            }
            Logout();
        }

        static void client_OnDeviceDetails(zVirtualClient.Models.DeviceDetails DeviceDetailsResponse)
        {
            if (DeviceDetailsResponse.success)
            {
                Console.WriteLine(string.Format("Device Details:\ncool_p:\t\t{0}\nesm:\t\t{1}\nfan_mode:\t\t{2}\nfan_state:\t\t{3}\ngroups:\t\t{4}\nheat_p:\t\t{5}\nid:\t\t{6}\nlast_heard_from:\t\t{7}\nlevel:\t\t{8}\nlevel_txt:\t\t{9}\nmode:\t\t{10}\nname:\t\t{11}\non_off:\t\t{12}\nop_state:\t\t{13}\nplugin_name:\t\t{14}\ntype:\t\t{15}\ntype_txt:\t\t{16}",
                    DeviceDetailsResponse.details.cool_p,
                    DeviceDetailsResponse.details.esm,
                    DeviceDetailsResponse.details.fan_mode,
                    DeviceDetailsResponse.details.fan_state,
                    DeviceDetailsResponse.details.groups,
                    DeviceDetailsResponse.details.heat_p,
                    DeviceDetailsResponse.details.id,
                    DeviceDetailsResponse.details.last_heard_from,
                    DeviceDetailsResponse.details.level,
                    DeviceDetailsResponse.details.level_txt,
                    DeviceDetailsResponse.details.mode,
                    DeviceDetailsResponse.details.name,
                    DeviceDetailsResponse.details.on_off,
                    DeviceDetailsResponse.details.op_state,
                    DeviceDetailsResponse.details.plugin_name,
                    DeviceDetailsResponse.details.type,
                    DeviceDetailsResponse.details.type_txt
                    ));
            }
            else
            {
                Console.WriteLine(string.Format("Device Details query was not successful"));
            }
            Logout();
        }

        static void client_OnDeviceCommands(zVirtualClient.Models.DeviceCommands DeviceCommandsResponse)
        {
            if (DeviceCommandsResponse.success)
            {
                Console.WriteLine(string.Format("Device Commands:"));
                foreach (var item in DeviceCommandsResponse.device_commands)
                {
                    Console.WriteLine(string.Format("{0} : {1}", item.friendlyname, item.helptext));
                }
            }
            else
            {
                Console.WriteLine(string.Format("Device Commands query was not successful"));
            }
            Logout();
        }

        static void client_OnDeviceCommand(zVirtualClient.Models.DeviceCommandResponse DeviceCommandResponse)
        {
            Console.WriteLine(string.Format("Device Command Status:{0}, Reason:{1}", DeviceCommandResponse.success, DeviceCommandResponse.reason));
            Logout();
        }

        static void client_OnCommands(zVirtualClient.Models.CommandsResponse CommandsResponse)
        {
            if (CommandsResponse.success)
            {
                Console.WriteLine(string.Format("Device Command:"));
                foreach (var item in CommandsResponse.builtin_commands)
                {
                    Console.WriteLine(string.Format("{0} : {1}",item.friendlyname, item.helptext));
                }
            }
            else
            {
                Console.WriteLine(string.Format("Device Commands query was not successful"));
            }
            Logout();
        }

        static void client_OnChangeSceneName(zVirtualClient.Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            if (SceneNameChangeResponse.success)
            {
                Console.WriteLine(string.Format("Change Scene name was successful, {0}", SceneNameChangeResponse.desc));
            }
            else
            {
                Console.WriteLine(string.Format("Change Scene name was successful, {0}", SceneNameChangeResponse.desc));
            }
            Logout();
        }

        static void client_OnLogin(zVirtualClient.Models.LoginResponse LoginResponse)
        {
            if (LoginResponse.success)
            {
                Console.WriteLine("Login was a success");
                TakeAction();
            }
            else
            {
                Console.WriteLine("Could not authenticate your credentails against the Server");
                running = false;
            }

        }

        static void client_OnError(object Sender, string Message, Exception Exception)
        {
            Console.WriteLine(Message);
            Console.WriteLine(Exception.ToString());
            running = false;
        }
    }
}