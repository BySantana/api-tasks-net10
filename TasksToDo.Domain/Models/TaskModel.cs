using TasksToDo.Domain.Models.Base;

namespace TasksToDo.Domain.Models;

public class TaskModel : BaseEntity
{
    public TaskModel(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));

        Title = title;
        Description = description;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }

    private readonly List<TagModel> _tags = [];
    public IReadOnlyCollection<TagModel> Tags => _tags.AsReadOnly();

    #region Methods

    public void ChangeTask(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));

        Title = title;
        Description = description;
        SetUpdatedAt();
    }

    public void AddTag(TagModel tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        if (!_tags.Contains(tag))
            _tags.Add(tag);
    }

    public void RemoveTag(TagModel tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        _tags.Remove(tag);
    }

    #endregion
}