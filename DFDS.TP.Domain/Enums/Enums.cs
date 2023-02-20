namespace DFDS.TP.Domain.Enums
{
    /// <summary>
    /// Dummy class functioning as a contailer file for enums available in all of configuration
    /// </summary>
    public class Enums
    { }

    public enum YesOrNo { Yes, No }

    public enum EnabledOrDisabled { Enabled, Disabled }

    public enum OnOrOff { On, Off }

    public enum SlopeDirection { Positive, Negative }

    public enum RelativeOrAbsolute { Relative, Absolute }

    public enum PercentageOrVoltage { Percentage, Voltage }

    public enum ReadExcelDataAs { Row, Column }

    public enum PulseMode
    {
        None = 0,
        Recording0,
        Recording1,
        Analyze0,
        Remote
    }

    public enum PulseAnalysisMode
    {
        None,
        Monitor,
        Events
    }

    public enum ConfigSourceType
    {
        Unknown,
        System,
        Database,
        ExcelFile,
        XmlFile,
        CsvFile,
        HardwareScan,
        PulseExport,
        DomainModelFromAssembly
    }

    //The old list of configuration sections. Still used to identify configurations-sections for now, and for 
    //double checking that all sections are read genericly when implemented
    public enum ConfigSectionName
    {
        Metadata,
        DatxFields,
        Signals,
        CommonAnalysisSetup,
        MonitorAnalysisSetup,
        OperatorAnalysisSetup,
        PlaybackAnalysisSetup,
        RecAnalysisSetup, //TODO: Change to DAQAnalysis
        PostAnalysisSetup,
        PostVerificationAnalysisSetup,
        Events,
        AutoRecording,
        Logbook,
        MeasurementSetup,
        RelayBoxSetup,
        Tacho,
        FFT,
        Order,
        CPB,
        FFTMonitor,
        OrderMonitor,
        CPBMonitor,
        DACScribe,
        SatelliteTestRandomAnalyzer,
        SatelliteTestSweptSineAnalyzer,
        SatelliteTestShockAnalyzer,
        SatelliteTestAcousticFatigueAnalyzer,
        ReferenceGroups,
        SignalTrigger,
        COLA,
        ReferenceProfiles
    }

    public enum Slope
    {
        Unknown,
        Positive,
        Negative
    }

    //TODO: Test if this method of using flags for aggregated groups works in PulseAnalysisSetupModel, replacing an enum and a method
    [Flags]
    public enum AnalysisGroupCategory
    {
        None = 0,
        Monitor = 1,
        Operator = 2,
        Playback = 4,
        Post = 8,
        PostVerify = 16,
        RecAnalysis = 32, //TODO
        Events = 64,
        Common = 128,
        RefGroups = 256,

        //AnalysisModeMonitor = Monitor | Operator | Playback,
        //AnalysisModeRecAnalysis = RecAnalysis,
        //AnalysisModeRecEvents = Events,
        //AnalysisModeNone = Common,

        //ForAcqusitionStation = Common | Monitor | RecAnalysis | Events,
        //ForMonitorStation = Common | Monitor,
        //ForOperatorStation = Common | Operator,
        //ForAcqusitionPlayback = Common | Playback
    }

    [Flags]
    public enum ValidationResult
    {
        [Display(Description = "Validation result is unknown. The cause could be a system error.")]
        Unknown = 0,

        [Display(Description = "Validation is completed without errors.")]
        CompletedWithoutErrors = 1,

        [Display(Description = "Validation is completed with minor errors, mostly handled by using default settings.")]
        CompletedWithNonfatalErrors = 2,

        [Display(Description = "Validation is completed, but there are errors that needs to be adressed for the DAQ-system to function.")]
        CompletedWithFatalErrors = 4,

        [Display(Description = "Validation couldn't be completed. could be caused by missing values or system settings.")]
        CouldNotBeCompleted = 8,
        
        [Display(Description = "Validation was interrupted by user and not completed. Should be run again.")]
        CancelledByUser = 16,

        [Display(Name = "Any completed", Description = "Any result where validation was completed")]
        AnyCompleted = CompletedWithoutErrors | CompletedWithNonfatalErrors | CompletedWithFatalErrors,

        [Display(Name = "Any not completed", Description = "Any result where validation was not completed")]
        AnyNotCompleted = Unknown | CouldNotBeCompleted | CancelledByUser
    }

    public enum SpeedTriggerModes
    {
        [Display(Name = "ENTER NAME")]
        Unknown,
        [Display(Name = "ENTER NAME")]
        RunupRundown,
        [Display(Name = "ENTER NAME")]
        Runup,
        [Display(Name = "ENTER NAME")]
        Rundown
    }

    public enum AutomaticRecordingModes
    {
        [Display(Name = "Unknown")]
        Unknown,

        [Display(Name = "Speed Start/Stop Mode")]
        SpeedStartStop,

        /// <summary>
        /// Speed Start mode. Uses Speed start trigger and stops after specified recording time.
        /// </summary>
        [Display(Name = "ENTER NAME")]
        SpeedStart,

        /// <summary>
        /// Level start/stop mode.
        /// Recording is started the signal level is above the start level, and stopped when signal level is below the stop level.
        /// </summary>
        [Display(Name = "ENTER NAME")]
        LevelStartStop,

        /// <summary>
        /// Level start mode.
        /// Recording is started the signal level is above the start level and stopped when recoding time reaches Recoridng length.
        /// </summary>
        [Display(Name = "ENTER NAME")]
        LevelStart,

        /// <summary>
        /// Alarm start/stop mode.
        /// Recording is started when any of the specified alarms are set and stopped when all alarms are cleared.
        /// </summary>
        AlarmStartStop,

        /// <summary>
        /// Alarm start mode.
        /// Recording is started when any of the specified alarms are set and stopped when recoding time reaches Recoridng length.
        /// </summary>
        [Display(Name = "ENTER NAME")]
        AlarmStart,

        /// <summary>
        /// Digital input start/stop mode.
        /// Recording is started when any of the specified digital inputs are high and stopped when all digital inputs are low
        /// </summary>
        [Display(Name = "ENTER NAME")]
        DigitalInputStartStop,

        /// <summary>
        /// Digital input start mode.
        /// Recording is started when any of the specified digital inputs are high and stopped when recoding time reaches Recoridng length
        /// </summary>
        [Display(Name = "ENTER NAME")]
        DigitalInputStart,

        /// <summary>
        /// The manual auto mode
        /// </summary>
        [Display(Name = "ENTER NAME")]
        Manual
    }
}
