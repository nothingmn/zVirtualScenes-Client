using System;
using System.Collections;
using System.IO.Ports;
using Agent.Contrib.Communication.Channels;
using Agent.Contrib.Util;
using Microsoft.SPOT;

namespace AGENT.Automation.Watch
{
    public class AutomationChannel : CSVChannel, IChannel
    {
        public string CommandType { get; set; }
        public string SceneName { get; set; }
        public bool LastSceneExecutedCorrectly { get; set; }
        public Hashtable SceneList { get; set; }


        public AutomationChannel()
        {
            SceneList = new Hashtable();
        }

        public object Read(System.IO.Ports.SerialPort port)
        {
            string[] payload = (string[]) base.Read(port);

            //Two options for CommandType
            // 1:  "SceneList"
            // A list of scenes
            // The rest of the CSV data will be a name value pair of each scene and ID
            // they will be in the format of a |'d delimited string of NAME|ID
            // 2. SceneResult
            // The result of the last executed scene
            // payload[1] will either be true or false 

            CommandType = payload[0].ToLower();

            if (CommandType == "scenelist")
            {
                SceneList.Clear();

                for (int i = 1; i < payload.Length; i++)
                {
                    string[] both = payload[i].Split('|');
                    SceneList.Add(both[0], both[1]);
                }
            }
            else if (CommandType == "sceneresult")
            {
                bool result = false;
                Parse.TryParseBool(payload[1], out result);
                LastSceneExecutedCorrectly = result;
            }

            return this;
        }

        public void ExecuteScene(SerialPort port, string sceneName)
        {
            Write(port, new string[] {"executescene", sceneName});
        }

        public void Write(System.IO.Ports.SerialPort port, object data)
        {
            base.Write(port, data);
        }
    }
}