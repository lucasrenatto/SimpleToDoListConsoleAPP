internal class Program
{
    public static void Main(string[] args)
    {

        bool programFinish = false;
        int lastID = 0;
        int option = 0;
        bool isOptionValid = false;
        int editTask = 0;
        int finishedTask = 0;
        bool isFirst = false;
        string userName = "";
        string taskShow = "";
        List<Task> tasks = new List<Task>();
        TaskModel task = new TaskModel();
        while (!programFinish)
        {
            if (isFirst == false)
            {
                lastID = 1;
                task = new TaskModel();
                Console.WriteLine("Olá bem vindo ao Gerenciador de Tarefas LRDEV");
                Console.WriteLine("Vi aqui e é a primeira vez que você abre a aplicação :o");
                Console.WriteLine("Pra gente começar, me diga, como posso te chamar?");
                userName = Console.ReadLine();
                Console.WriteLine($"É um prazer te conhecer {userName}");
                Console.WriteLine("Então vamos criar sua primeira tarefa");
                Console.WriteLine("Primeiro me diga uma titulo para a sua tarefa");
                task.Title = Console.ReadLine();
                Console.WriteLine("Que bacana e qual descrição você quer dar a essa tarefa?");
                task.Description = Console.ReadLine();
                Console.WriteLine("Essa tarefa você tem que fazer até quando? o formato deve ser : DD/MM/AAAA");
                task.DueDate = DateOnly.Parse(Console.ReadLine());
                NewTask(task, tasks, lastID);
                Console.WriteLine("Parabéns você acaba de criar sua primeira tarefa! Fique a vontade para criar outras!");
                isFirst = true;
            }
            Console.WriteLine("Essas são suas tarefas:");
            foreach (var t in tasks.Where(y => y.isComplete == false).ToList().OrderBy(x => x.DueDate))
            {
                taskShow = $@"•ID: {t.TaskID}" + "\n" + $"•Titulo: {t.Title}" + "\n" + $"•Descrição: {t.Description}" + "\n" + $"•Vencimento: {t.DueDate}";
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(taskShow);
                Console.WriteLine("---------------------------------------------------");
            }

            while (!isOptionValid)
            {
                task = new TaskModel();
                Console.WriteLine("Selecione uma Opção onde:");
                Console.WriteLine("[1] Nova Tarefa - [2] Editar Tarefa - [3] Concluir Tarefa - [4] Finalizar");
                option = Convert.ToInt32(Console.ReadLine());
                if (option != 1 && option != 2 && option != 3 && option != 4)
                    continue;
                else
                {
                    isOptionValid = true;
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Primeiro me diga uma titulo para a sua tarefa");
                            task.Title = Console.ReadLine();
                            Console.WriteLine("Que bacana e qual descrição você quer dar a essa tarefa?");
                            task.Description = Console.ReadLine();
                            Console.WriteLine("Essa tarefa você tem que fazer até quando? o formato deve ser : DD/MM/AAAA");
                            task.DueDate = DateOnly.Parse(Console.ReadLine());
                            NewTask(task, tasks, lastID + 1);
                            break;
                        case 2:
                            Console.WriteLine("Qual o ID da tarefa que você gostaria de editar?");
                            editTask = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Primeiro me diga uma titulo para a sua tarefa");
                            task.Title = Console.ReadLine();
                            Console.WriteLine("Que bacana e qual descrição você quer dar a essa tarefa?");
                            task.Description = Console.ReadLine();
                            Console.WriteLine("Essa tarefa você tem que fazer até quando? o formato deve ser : DD/MM/AAAA");
                            task.DueDate = DateOnly.Parse(Console.ReadLine());
                            EditTask(task, tasks, editTask);
                            break;
                        case 3:
                            Console.WriteLine("Qual o ID da tarefa que você concluiu?");
                            finishedTask = Convert.ToInt32(Console.ReadLine());
                            FinishTask(tasks, finishedTask);
                            break;
                        case 4:
                            programFinish = true;
                            break;
                    }
                }
            }
            isOptionValid = false;
        }
    }





    static List<Task> NewTask(TaskModel task, List<Task> tasks, int taskID)
    {
        Task taskEntity = new Task()
        {
            Description = task.Description,
            DueDate = task.DueDate,
            Title = task.Title,
            TaskID = taskID,
            isComplete = false
        };
        tasks.Add(taskEntity);
        return tasks;
    }
    static List<Task> EditTask(TaskModel task, List<Task> tasks, int taskID)
    {
        Task taskEntity = new Task()
        {
            Description = task.Description,
            DueDate = task.DueDate,
            Title = task.Title,
            TaskID = taskID
        };
        tasks.RemoveAll(x => x.TaskID == taskID);
        tasks.Add(taskEntity);
        return tasks;
    }
    static List<Task> FinishTask(List<Task> tasks, int taskID)
    {
        var target = tasks.Where(x => x.TaskID == taskID).FirstOrDefault();
        Task taskEntity = new Task()
        {
            Description = target.Description,
            DueDate = target.DueDate,
            Title = target.Title,
            TaskID = taskID,
            isComplete = true
        };
        tasks.RemoveAll(x => x.TaskID == taskID);
        tasks.Add(taskEntity);
        return tasks;
    }
}


public class Task
{
    public int TaskID { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateOnly DueDate { get; set; }
    public bool isComplete { get; set; }
}

public class TaskModel
{
    public int TaskID { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateOnly DueDate { get; set; }
    public bool isComplete { get; set; }
}
