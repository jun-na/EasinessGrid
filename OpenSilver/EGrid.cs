using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

public class EGrid : Grid
{
    private static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register("Columns",
            typeof(string),
            typeof(EGrid),
            new PropertyMetadata(string.Empty, OnColumnsChanged));

    /// <summary>
    /// カラム定義プロパティ
    /// </summary>
    public string Columns
    {
        get
        {
            return (string)GetValue(ColumnsProperty);
        }
        set
        {
            SetValue(ColumnsProperty, value);
        }
    }

    private static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register(
            "Rows",
            typeof(string),
            typeof(EGrid),
            new PropertyMetadata(string.Empty, OnRowsChanged));

    /// <summary>
    /// ロウ定義プロパティ
    /// </summary>
    public string Rows
    {
        get
        {
            return (string)GetValue(RowsProperty);
        }
        set
        {
            SetValue(RowsProperty, value);
        }
    }

    private static GridLength ToGridLength(string str)
    {
        // 4.0*のような文字列から数値のみ正規表現で取得
        var value = 0.0;
        double.TryParse(Regex.Match(str, @"[0-9.]+").Value, out value);

        var result = str switch
        {
            string s when s.EndsWith("*") => new GridLength((value==0.0 ? 1.0 : value), GridUnitType.Star),
            string s when s.EndsWith("Auto") => new GridLength(0, GridUnitType.Auto),
            _ => new GridLength(value, GridUnitType.Pixel),
        };
        return result;
    }

    /// <summary>
    /// Columns変更時の処理
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Grid取得
        var grid = d as EGrid;
        if (grid == null)
        {
            return;
        }

        // カラム定義取得
        var strArray = d.GetValue(ColumnsProperty) as string;
        if (strArray == null)
        {
            return;
        }


        // カラム定義をGridに設定
        grid.ColumnDefinitions.Clear();
        foreach (var item in strArray.Split(","))
        {
            // 文字列をGridLengthに変換
#pragma warning disable CS8605 // null の可能性がある値をボックス化解除しています。
            grid.ColumnDefinitions.Add(value: new ColumnDefinition() { Width = ToGridLength(item) });
#pragma warning restore CS8605 // null の可能性がある値をボックス化解除しています。
        }

        // レイアウトを更新
        grid.UpdateLayout();
    }


    /// <summary>
    /// Rows変更時の処理
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Grid取得
        var grid = d as EGrid;
        if (grid == null)
        {
            return;
        }

        // ロウ定義取得
        var strArray = d.GetValue(RowsProperty) as string;
        if (strArray == null)
        {
            return;
        }

        // ロウ定義をGridに設定
        grid.RowDefinitions.Clear();
        foreach (var item in strArray.Split(","))
        {
            // 文字列をGridLengthに変換
#pragma warning disable CS8605 // null の可能性がある値をボックス化解除しています。
            grid.RowDefinitions.Add(new RowDefinition() { Height = ToGridLength(item) });
#pragma warning restore CS8605 // null の可能性がある値をボックス化解除しています。
        }

        // レイアウトを更新
        grid.UpdateLayout();
    }
}

