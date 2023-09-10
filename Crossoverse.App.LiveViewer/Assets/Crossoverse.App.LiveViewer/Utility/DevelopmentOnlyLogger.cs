namespace Crossoverse.App.LiveViewer
{
    public static class DevelopmentOnlyLogger
    {
        /// <summary>
        /// Logs a message to the console 
        /// only when DEVELOPMENT_BUILD or UNITY_EDITOR is defined.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [
            System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), 
            System.Diagnostics.Conditional("UNITY_EDITOR"),
        ]
        public static void Log(object message) => UnityEngine.Debug.Log(message);

        /// <summary>
        /// Logs a warning message to the console 
        /// only when DEVELOPMENT_BUILD or UNITY_EDITOR is defined.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [
            System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), 
            System.Diagnostics.Conditional("UNITY_EDITOR"),
        ]
        public static void LogWarning(object message) => UnityEngine.Debug.LogWarning(message);

        /// <summary>
        /// Logs an error message to the console 
        /// only when DEVELOPMENT_BUILD or UNITY_EDITOR is defined.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [
            System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), 
            System.Diagnostics.Conditional("UNITY_EDITOR"),
        ]
        public static void LogError(object message) => UnityEngine.Debug.LogError(message);
    }
}
