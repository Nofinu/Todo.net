using System;

namespace ContactList.Classes;

public class Todo
{
    private string _title;
    private string _description;
    private DateTime _date;
    private Boolean _status;

    public Todo()
    {
    }

    public Todo(string title, string description,DateTime date)
    {
        _title = title;
        _description = description;
        _date = date;
        _status = false;
    }

    public string Title
    {
        get => _title;
        set => _title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public bool Status
    {
        get => _status;
        set => _status = value;
    }
}