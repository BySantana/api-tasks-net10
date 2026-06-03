using TasksToDo.Domain.Models.Base;

namespace TasksToDo.Domain.Models;

public class TagModel : BaseEntity
{
    public TagModel(string title)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));

        Title = title;
    }
    
    public string Title { get; private set; }

    private readonly List<TaskModel> _tasks = [];
    public IReadOnlyCollection<TaskModel> Tasks => _tasks.AsReadOnly();

    public void ChangeTag(string title)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        
        Title = title;
        SetUpdatedAt();
    }
}