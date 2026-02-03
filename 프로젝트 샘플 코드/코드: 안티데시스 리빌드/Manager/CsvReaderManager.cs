using System.Collections.Generic;
using System.IO;
using UnityEngine;
using CsvHelper;
using System.Globalization;

public class CsvReaderManager : MonoBehaviour
{
    public static CsvReaderManager Instance;
    public Dictionary<string, Dialogue> npcDialogue = new Dictionary<string, Dialogue>();

    // CSV 헤더와 동일하게 작성
    [System.Serializable]
    public struct Dialogue
    {
        // dialogueID: 대사 번호
        public string dialogueID { get; set; }

        // phase: 해당 대사가 속한 페이즈
        public string phase { get; set; }

        // order: 대사가 출력되는 순서
        public string order { get; set; }

        // text: 대사 내용
        public string text { get; set; }
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string path = Application.dataPath + "/Script/Dialogue/Npc_Dialogue.csv";
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Dialogue>();
            foreach (var dialogue in records)
            {
                npcDialogue.Add(dialogue.dialogueID, dialogue);
            }
        }
    }
}
