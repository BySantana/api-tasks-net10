namespace TasksToDo.Models;

public class TagModel
{
    public TagModel(string title)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        
        Id = Guid.NewGuid();
        Title = title;
    }
    
    public Guid Id { get; init; }
    public string Title { get; private set; }

    private readonly List<TaskModel> _tasks = [];
    public IReadOnlyCollection<TaskModel> Tasks => _tasks.AsReadOnly();

    public void ChangeTag(string title)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        
        Title = title;
    }
}