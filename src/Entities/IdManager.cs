public class IdManager {
    int nextId = 0;
    private Queue<int> freeIds;
    private HashSet<int> usedIds;

    public IdManager() {
        freeIds = new Queue<int>();
        usedIds = new HashSet<int>();
    }

    public int generateId() {
        int generatedId = (freeIds.Count > 0) ? freeIds.Dequeue() : nextId++;

        usedIds.Add(generatedId);
        return generatedId;
    }

    public void freeId(int id) {
        if (usedIds.Remove(id))
            freeIds.Enqueue(id);
    }

    public void clearIds() {
        nextId = 0;
        freeIds.Clear();
        usedIds.Clear();
    }
} 