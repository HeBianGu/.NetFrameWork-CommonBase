using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.ValidAttribute
{
    public enum UnitInputMode
    {
        Freq,
        FreqOnly,
        Time,
        Power,
        Percent,
        CodeRate,
        SweepCycle,
        SweepRate,
        Degree,
        SNR,
        None,
        Rate,
        Binary,
        ZeroToThree,
        CurrentUnits,
        VoltageUnits,
    }
    public enum DegreeUnits
    {
        [Description("°")]
        DegreeValue,
    }
    public enum RateUnits
    {
        [Description("km/h")]
        kilometer
    }
    public enum CodeRateUnits
    {
        sps,
        Ksps,
        Msps,
        Gsps
    }
    public enum FrequencyUnits
    {
        Hz,
        kHz,
        MHz,
        GHz
    }
    public enum TimeUnits
    {
        s,
        ms,
        μs,
        ns
    }
    public enum SweepRateUnits
    {
        [Description("°/s")]
        Second,
    }
    public enum PowerUnits
    {
        dBm,
    }
    public enum SNRUnits
    {
        dB,
    }
    public enum SweepCycleUnit
    {
        rpm,
    }
    public enum PercentUnits
    {
        hundredth,


    }

    /// <summary> 电流单位 </summary>
    public enum CurrentUnits
    {
        kA,
        A,
        mA,
        μA
    }

    /// <summary> 电压单位 </summary>
    public enum VoltageUnits
    {
        kV,
        V,
        mV,
        μV,
        nV
    }

}
