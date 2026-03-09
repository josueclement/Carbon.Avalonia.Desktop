using System.ComponentModel.DataAnnotations;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarbonAvaloniaDesktopTester.ViewModels;

public class EditorsTestingPageViewModel : ObservableValidator
{
    public string? TextValueInfo          { get; private set => SetProperty(ref field, value); }
    public string? IntValueInfo           { get; private set => SetProperty(ref field, value); }
    public string? UintValueInfo          { get; private set => SetProperty(ref field, value); }
    public string? ShortValueInfo         { get; private set => SetProperty(ref field, value); }
    public string? UshortValueInfo        { get; private set => SetProperty(ref field, value); }
    public string? LongValueInfo          { get; private set => SetProperty(ref field, value); }
    public string? UlongValueInfo         { get; private set => SetProperty(ref field, value); }
    public string? SingleValueInfo        { get; private set => SetProperty(ref field, value); }
    public string? DoubleValueInfo        { get; private set => SetProperty(ref field, value); }
    public string? DecimalValueInfo       { get; private set => SetProperty(ref field, value); }
    public string? MultiLineTextValueInfo { get; private set => SetProperty(ref field, value); }
    public string? BytesValueInfo           { get; private set => SetProperty(ref field, value); }

    [Required]
    [MaxLength(100)]
    public string? TextValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                TextValueInfo = value?.ToString() ?? "null";
        }
    } = "Hello world";

    [Range(0, 10000)]
    public int? IntValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                IntValueInfo = value?.ToString() ?? "null";
        }
    } = 42;

    [Range(1u, 999999u)]
    public uint? UintValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                UintValueInfo = value?.ToString() ?? "null";
        }
    } = 42;

    [Range((short)-1000, (short)1000)]
    public short ShortValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                ShortValueInfo = value.ToString();// ?? "null";
        }
    } = -100;

    [Range((ushort)1, (ushort)65535)]
    public ushort? UshortValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                UshortValueInfo = value?.ToString() ?? "null";
        }
    } = 500;

    [Range(typeof(long), "0", "9223372036854775807")]
    public long? LongValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                LongValueInfo = value?.ToString() ?? "null";
        }
    } = 9876543210;

    [Range(typeof(ulong), "1", "1048576")]
    public ulong? UlongValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                UlongValueInfo = value?.ToString() ?? "null";
        }
    } = 1024;

    [Range(0.0, 100.0)]
    public float? SingleValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                SingleValueInfo = value?.ToString() ?? "null";
        }
    } = 2.718f;

    [Range(0.0, 99999.99)]
    public double? DoubleValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                DoubleValueInfo = value?.ToString() ?? "null";
        }
    } = 3.14;

    [Range(typeof(decimal), "0.01", "99999.99")]
    public decimal? DecimalValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                DecimalValueInfo = value?.ToString() ?? "null";
        }
    } = 99.95m;

    [Required]
    [MinLength(5)]
    public string? MultiLineTextValue {
        get;
        set {
            if (SetProperty(ref field, value, true))
                MultiLineTextValueInfo = value?.ToString() ?? "null";
        }
    } = "Line one\nLine two\nLine three";

    public byte[]? BytesValue {
        get;
        set {
            if (SetProperty(ref field, value))
                BytesValueInfo = value is not null ? $"{value.Length} bytes" : "null";
        }
    } = [0x0A, 0xFF, 0x1B, 0x42, 0xDE, 0xAD, 0xBE, 0xEF];
}
