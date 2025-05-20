public class TarefaListBoxItem
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Status { get; set; }
    public string DataConclusao { get; set; }

    public override string ToString()
    {
        // Exibe apenas o título, status e data de conclusão (sem o ID)
        return $"{Titulo} ({Status}{DataConclusao})";
    }
}