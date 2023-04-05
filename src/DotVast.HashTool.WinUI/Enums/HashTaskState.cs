using System.Text.Json.Serialization;

namespace DotVast.HashTool.WinUI.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HashTaskState
{
    /// <summary>
    /// 等待中(从未进行过计算).
    /// </summary>
    Waiting,

    /// <summary>
    /// 计算中(正在计算, 未暂停).
    /// </summary>
    Working,

    /// <summary>
    /// 已暂停(正常计算期间暂停).
    /// </summary>
    Paused,

    /// <summary>
    /// 已完成(正常完成计算).
    /// </summary>
    Completed,

    /// <summary>
    /// 已取消(用户主动).
    /// </summary>
    Canceled,

    /// <summary>
    /// 已中止(错误/意外).
    /// </summary>
    Aborted,
}

public static class HashTaskStateExtensions
{
    public static string ToDisplay(this HashTaskState hashTaskState)
    {
        return hashTaskState switch
        {
            HashTaskState.Waiting => new(LocalizationEnum.HashTaskState_Waiting),
            HashTaskState.Working => new(LocalizationEnum.HashTaskState_Working),
            HashTaskState.Paused => new(LocalizationEnum.HashTaskState_Paused),
            HashTaskState.Completed => new(LocalizationEnum.HashTaskState_Completed),
            HashTaskState.Canceled => new(LocalizationEnum.HashTaskState_Canceled),
            HashTaskState.Aborted => new(LocalizationEnum.HashTaskState_Aborted),
            _ => throw new ArgumentOutOfRangeException(nameof(hashTaskState)),
        };
    }
}
