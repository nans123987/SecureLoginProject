using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SecureLoginService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISecureLoginService" in both code and config file together.
    [ServiceContract]
    public interface ISecureLoginService
    {

        [OperationContract]
        bool RegisterUser(string Username, string Password, string emailId, string DateofBirth);

        [OperationContract]
        bool LoginValidation(string Username, string Password);

        [OperationContract]
        bool checkIfUserExists(string Username);

        [OperationContract]
        bool checkIfAccountLocked(string Username);
        
    }
}
