public class TarefaListBoxItem
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Status { get; set; }
    public string DataConclusao { get; set; }

    public override string ToString()
    {
        return $"{Titulo} ({Status}{DataConclusao})";
    }
}