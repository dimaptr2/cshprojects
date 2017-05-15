﻿using System.Windows.Forms;
using System.Collections.Generic;
using SAP.Middleware.Connector;

namespace SAPEntity {

    public class SAPReader
    {
        private static SAPReader instance;
        private Dictionary<string, string> connection;
        private RfcConfigParameters parameters;
        private RfcDestination destination;

        private SAPReader(Dictionary<string, string> connection)
        {
            this.connection = connection;
            parameters = new RfcConfigParameters();
            parameters[RfcConfigParameters.Name] = "PRD500";
            parameters[RfcConfigParameters.AppServerHost] = connection["ashost"];
            parameters[RfcConfigParameters.SystemNumber] = connection["sysnr"];
            parameters[RfcConfigParameters.SystemID] = connection["r3name"];
            parameters[RfcConfigParameters.Client] = connection["client"];
            parameters[RfcConfigParameters.User] = connection["user"];
            parameters[RfcConfigParameters.Password] = connection["password"];
            parameters[RfcConfigParameters.Language] = connection["lang"];
        }
        
        // Get a singleton
        public static SAPReader getInstance(Dictionary<string, string> connection)
        {

            if (instance == null)
            {
                instance = new SAPReader(connection);
            }

            return instance;
        }

        public void initSAPDestionation()
        {
            connection.Clear();

            if (parameters != null)
            {
                try
                {
                    destination = RfcDestinationManager.GetDestination(parameters);
                } catch (RfcInvalidParameterException ex)
                {
                    MessageBox.Show(ex.Message);
                } catch (RfcBaseException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

        }

    } // end of class

  

}  // end of namespace