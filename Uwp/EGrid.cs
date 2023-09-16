using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

/// <summary>
/// グリッドのカスタムコントロール
/// </summary>
internal class EGrid : Grid {

    private static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register("Columns",
            typeof(string),
            typeof(EGrid),
            new PropertyMetadata(string.Empty, OnColumnsChanged));

    /// <summary>
    /// カラム定義プロパティ
    /// </summary>
    public string Columns {
        get {
            return GetValue(ColumnsProperty) as string;
        }
        set {
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
    public string Rows {
        get {
            return GetValue(RowsProperty) as string;
        }
        set {
            SetValue(RowsProperty, value);
        }
    }


    /// <summary>
    /// Columns変更時の処理
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        // Grid取得
        var grid = d as EGrid;

        // カラム定義取得
        var strArray = d.GetValue(ColumnsProperty) as string;

        // カラム定義をGridに設定
        grid.ColumnDefinitions.Clear();
        foreach (var item in strArray.Split(",")) {
            // 文字列をGridLengthに変換
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = (GridLength)XamlBindingHelper.ConvertValue(typeof(GridLength), item) });
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
    private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        // Grid取得
        var grid = d as EGrid;

        // ロウ定義取得
        var strArray = d.GetValue(RowsProperty) as string;

        // ロウ定義をGridに設定
        grid.RowDefinitions.Clear();
        foreach (var item in strArray.Split(",")) {
            // 文字列をGridLengthに変換
            grid.RowDefinitions.Add(new RowDefinition() { Height = (GridLength)XamlBindingHelper.ConvertValue(typeof(GridLength), item) });
        }

        // レイアウトを更新
        grid.UpdateLayout();
    }
}
