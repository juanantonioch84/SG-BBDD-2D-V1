using System.Collections;
using System.Collections.Generic;

public static class NotesData
{
    /*
    public static NoteModel b1_n1 = new NoteModel(
        "Todo profesor contratado por la Universidad pertenece a un único departamento.",
        new string[] { "profesor", "pertenece a", "departamento" },
        new string[] { "profesor", "pertenece a", "departamento" }
    );

    private static NoteModel b1_n2 = new NoteModel(
        "Para entrar en el sistema, el profesor necesitaba un identificador que no recordaba, menos mal que también podía acceder usando su nombre completo.",
        new string[] { "profesor", "identificador", "nombre" },
        new string[] { "profesor", "identificador (1)", "nombre (1)" }
    );

    private static NoteModel b1_n3 = new NoteModel(
        "Los profesores no están demasiado contentos con los salarios, aunque poco se puede hacer con el presupuesto que tiene el departamento.",
        new string[] { "profesores", "salarios", "presupuesto", "departamento" },
        new string[] { "profesor", "salario", "presupuesto", "departamento" }
    );

    private static NoteModel b1_n4 = new NoteModel(
       "Cada uno de los grupos de una asignatura lo pueden impartir entre varios profesores. Además, parece que los grupos tienen un código identificativo.",
       new string[] { "profesores", "impartir", "grupos", "asignatura", "código" },
       new string[] { "profesor", "imparte", "grupo", "asignatura", "código (1)" }
    );

    private static NoteModel b1_n5 = new NoteModel(
       "Los aulas son asignadas por grupo, no por asignatura. Cabe destacar que los grupos siempre dan clase en el mismo aula.",
       new string[] { "aulas", "son asignadas", "grupos", "grupo", "asignatura", "aula" },
       new string[] { "aula", "es asignada", "grupo", "asignatura" }
    );

    private static NoteModel b2_n1 = new NoteModel(
        "Los departamentos están repartidos por todos el campus, me ha costado un buen rato encontrar el edificio adecuado.",
        new string[] { "departamentos", "edificio" },
        new string[] { "departamento", "edificio (1)" }
    );

    private static NoteModel b2_n2 = new NoteModel(
        "En la puerta del departamento había una placa con su nombre, esperaba ver un código pero parece que no tienen.",
        new string[] { "departamento", "nombre" },
        new string[] { "departamento", "nombre (2)" }
    );

    private static NoteModel b2_n3 = new NoteModel(
        "En el departamento también se han quejado de los presupuestos, parece que hay problemas de dinero.",
        new string[] { "departamento", "presupuestos" },
        new string[] { "departamento", "presupuesto" }
    );

    private static NoteModel b2_n4 = new NoteModel(
        "Se lleva un registro de qué profesores enseñan a qué alumnos.",
        new string[] { "profesores", "enseñan", "alumnos" },
        new string[] { "profesor", "enseña", "alumno" }
    );

    private static NoteModel b2_n5 = new NoteModel(
        "El jefe del departamento me ha mostrado un informe con las asignaturas de las que el departamento siempre es responsable. En el informe aparecía el código de la asignatura y el único departamento del que depende.",
        new string[] { "asignaturas", "departamento", "es responsable", "asignatura", "código" },
        new string[] { "asignatura", "departamento", "es responsable", "código (2)" }
    );

    private static NoteModel b3_n1 = new NoteModel(
        "A cada alumno se le proporciona un identificador único ya que podría haber varios alumnos con el mismo nombre.",
        new string[] { "alumnos", "identificador", "nombre", "alumno" },
        new string[] { "alumno", "identificador (2)", "nombre (3)" }
    );

    private static NoteModel b3_n2 = new NoteModel(
        "Para las clases de cada asignatura, se organizan uno o varios grupos específicos para ella.",
        new string[] { "asignatura", "se organizan", "grupos" },
        new string[] { "asignatura", "se organiza", "grupo" }
    );

    private static NoteModel b3_n3 = new NoteModel(
        "Los alumnos asisten a los diferentes grupos creados en un semestre de un año concreto. Por cada grupo al que asiste un alumno recibe una calificación de su correspondiente asignatura.",
        new string[] { "alumnos", "asisten a", "grupos", "semestre", "año", "calificaciones", "grupo", "alumno", "asiste", "calificación", "asignatura" },
        new string[] { "alumno", "asiste a", "grupo", "semestre", "año", "calificación", "asignatura" }
    );

    private static NoteModel b3_n4 = new NoteModel(
        "Cada asignatura tiene un título y un número de créditos, aunque no todos los alumnos se matriculan de la misma cantidad de créditos.",
        new string[] { "asignatura", "título", "créditos", "alumnos" },
        new string[] { "asignatura", "título", "créditos (1)", "alumno", "créditos (2)" }
    );

    private static NoteModel b3_n5 = new NoteModel(
        "La reunión ha sido en una de las aulas de los nuevos edificios del campus. En la puerta había un cartel con el número del aula y la capacidad máxima.",
        new string[] { "aulas", "edificios", "número", "aula", "capacidad" },
        new string[] { "aula", "edificio (2)", "número", "capacidad" }
    );
   
    private static Dictionary<string, NoteModel> _b1Notes = new Dictionary<string, NoteModel>() {
        {"b1-n1", NoteLoader.LoadNote("Data/Dictionary"},
        {"b1-n2", b1_n2},
        {"b1-n3", b1_n3},
        {"b1-n4", b1_n4},
        {"b1-n5", b1_n5}
    };

    private static Dictionary<string, NoteModel> _b2Notes = new Dictionary<string, NoteModel>() {
        {"b2-n1", b2_n1},
        {"b2-n2", b2_n2},
        {"b2-n3", b2_n3},
        {"b2-n4", b2_n4},
        {"b2-n5", b2_n5}
    };

    private static Dictionary<string, NoteModel> _b3Notes = new Dictionary<string, NoteModel>() {
        {"b3-n1", b3_n1},
        {"b3-n2", b3_n2},
        {"b3-n3", b3_n3},
        {"b3-n4", b3_n4},
        {"b3-n5", b3_n5}
    };

    */

    private static Dictionary<string, Dictionary<string, NoteModel>> _notes = new Dictionary<string, Dictionary<string, NoteModel>> {
        {"b1", NoteLoader.LoadNote(1)},
        {"b2", NoteLoader.LoadNote(2)},
        {"b3", NoteLoader.LoadNote(3)}
    };

    // Return a dictionary with the note codes and rich texts according to the received index
    public static Dictionary<string, string> GetNoteDictionary(string index)
    {
        Dictionary<string,string> richNotes = new Dictionary<string, string>();

        foreach (KeyValuePair<string, NoteModel> note in _notes[index]) {
            richNotes.Add(note.Key, note.Value.GetRichText());
        }

        return richNotes;
    }

    // Return a list with the rich texts of all notes
    public static List<string> GetAllNoteList()
    {
        List<string> richNotes = new List<string>();

        foreach (KeyValuePair<string, Dictionary<string, NoteModel>> note in _notes) {
            foreach (KeyValuePair<string, NoteModel> n in note.Value) {
                richNotes.Add(n.Value.GetRichText());
            }
        }

        return richNotes;
    }

    // Return a list with the words of the notes according to the received index
    public static List<string> GetWordList(string index)
    {
        List<string> words = new List<string>();

        foreach (KeyValuePair<string, NoteModel> note in _notes[index]) {

            foreach (string word in note.Value._words) {

                if (!words.Contains(word)) {
                    words.Add(word);
                }
            }
        }

        return words;
    }

    // Return a list with the words of the notes according to the received index
    public static List<string> GetAllWordList()
    {
        List<string> words = new List<string>();

        foreach (KeyValuePair<string, Dictionary<string, NoteModel>> note in _notes) {

            foreach (KeyValuePair<string, NoteModel> n in note.Value) {

                foreach (string word in n.Value._words) {

                    if (!words.Contains(word)) {
                        words.Add(word);
                    }
                }
            }
        }

        words.Sort();

        return words;
    }

    //public static Dictionary<string, NoteModel> GetNotes(string index)
    //{
    //    // PROCESS THE TEXT OF THE NOTES
    //    return ProcessNotes(_notes[index]);
    //}

    //public static Dictionary<string, NoteModel> GetAllNotes()
    //{
    //    Dictionary<string, NoteModel> allNotes = new Dictionary<string, NoteModel>();

    //    foreach (KeyValuePair<string, Dictionary<string, NoteModel>> note in _notes) {
    //        foreach (KeyValuePair<string,NoteModel> n in note.Value) {
    //            allNotes.Add(n.Key, n.Value);
    //        }
    //    }

    //    // PROCESS THE TEXT OF THE NOTES
    //    return ProcessNotes(allNotes);
    //}

    //private static Dictionary<string, NoteModel> ProcessNotes(Dictionary<string, NoteModel> notesToProcess)
    //{
    //    Dictionary<string, NoteModel> processedNotes = new Dictionary<string, NoteModel>();

    //    foreach (KeyValuePair<string, NoteModel> noteToProcess in notesToProcess) {
    //        noteToProcess.Value.ProcessNote();
    //        processedNotes.Add(noteToProcess.Key, noteToProcess.Value);
    //    }

    //    return processedNotes;
    //}
}
