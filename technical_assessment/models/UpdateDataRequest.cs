namespace Technical_assessment.models
{
    
    public class UpdateUserRequest
    {
        public Guid UserAuth { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UpdateTaskRequest
    {
        
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public string DueDate { get; set; }
    }
}
