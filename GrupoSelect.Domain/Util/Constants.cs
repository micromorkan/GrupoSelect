﻿namespace GrupoSelect.Domain.Util
{
    public static class Constants
    {
        #region PROFILES CONSTANTS

        public const string PROFILE_REPRESENTANTE = "REPRESENTANTE";
        public const string PROFILE_ADMINISTRATIVO = "ADMINISTRATIVO";
        public const string PROFILE_GERENTE = "GERENTE";
        public const string PROFILE_DIRETOR = "DIRETOR";
        public const string PROFILE_ADVOGADO = "ADVOGADO";
        public const string PROFILE_TI = "TI";

        #endregion

        #region ACCESS CONSTANTS

        public const string GS_ERRORS_ACCESS = "GS_ERRORS_ACCESS";
        public const string GS_AUTH_ERROR = "GS_AUTH_ERROR";

        #endregion

        #region FLUENT VALIDATION CONSTANTS

        public const string FLUENT_INSERT = "Insert";
        public const string FLUENT_UPDATE = "Update";
        public const string FLUENT_DELETE = "Delete";
        public const string FLUENT_AUTHENTICATE = "Authenticate";

        #endregion

        #region SYSTEM CONSTANTS
        
        public const string SYSTEM_CONN_STRING = "DefaultConnection";
        public const string SYSTEM_SETTINGS = "SystemSettings";
        public const string SYSTEM_SETTINGS_REGISTERSYSTEMLOG = "RegisterSystemLog";
        public const string SYSTEM_SETTINGS_REGISTERERRORLOG = "RegisterErrorLog";
        
        public const string SYSTEM_SUCCESS_MSG = "Operação realizada com sucesso!";
        public const string SYSTEM_ERROR_MSG = "Não foi possível executar esta ação. Contate a equipe de TI.";
        public const string SYSTEM_ERROR_ID = "O id informado não foi encontrado.";
        

        public const string SYSTEM_ERROR_KEY = "GS_ERROR";
        public const string SYSTEM_EXCEPTION_OBJ = "EXOBJECT";
        public static readonly string[] SYSTEM_IGNORE_AUDIT_TABLES = { "AccessLog", "SystemLog", "ErrorLog" };
        public const string SYSTEM_LOG_INSERT = "INSERT";
        public const string SYSTEM_LOG_UPDATE = "UPDATE";
        public const string SYSTEM_LOG_DELETE = "DELETE";

      

        #endregion
    }
}
