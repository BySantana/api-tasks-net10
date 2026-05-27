using TasksToDo.Models.Base;

namespace TasksToDo.Models;

public class TaskModel : BaseEntity
{
    public TaskModel(string title, Guid tagId, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        
        if(Guid.Empty == tagId)
            throw new ArgumentNullException(nameof(tagId));
        
        TagId = tagId;
        Title = title;
        Description = description;
    }
    public string Title { get; private set; }
    public string? Description { get; private set; }

    public Guid TagId { get; private set; }
    public TagModel Tag { get; private set; } = null!;
    
    #region Methods

    public void ChangeTask(string title, string? description, Guid tagId)
    {
        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));
        
        Title = title;
        Description = description;
        TagId = tagId;
        SetUpdatedAt();
    }
    
    #endregion
}