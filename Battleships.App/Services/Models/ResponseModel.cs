namespace Battleships.App.Services.Models;

public class ResponseModel
{
    public List<string> Errors { get; protected set; } = new List<string>();
    public string ErrorMessage => string.Join(';', Errors);
    public bool IsSuccess => !Errors.Any();

    protected ResponseModel() { }

    protected ResponseModel(params string[] errors)
    {
        Errors = errors.ToList();
    }

    public static ResponseModel ForSuccess()
    {
        return new();
    }

    public static ResponseModel ForError(params string[] errors)
    {
        return new(errors);
    }
}

public class ResponseModel<T> : ResponseModel
{
    public T? Result { get; private set; }

    public ResponseModel(T result) : base()
    {
        Result = result;
    }

    public ResponseModel(params string[] errors) : base(errors)
    {
    }

    public static ResponseModel<T> ForSuccess(T result)
    {
        return new ResponseModel<T>(result);
    }

    public static new ResponseModel<T> ForError(params string[] errors)
    {
        return new ResponseModel<T>(errors);
    }
}
