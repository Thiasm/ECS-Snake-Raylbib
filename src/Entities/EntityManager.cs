public class EntityManager {
    private IdManager idManager = new IdManager();
    private Dictionary<Type, Dictionary<int, IComponent>> componentList = new();

    public int createEntity() {
        int entityId = idManager.generateId();
        return entityId;
    }

    public void removeEntity(int entityId) {
        foreach (Type componentType in componentList.Keys) {
            if (componentList[componentType].Remove(entityId) == true) {
                idManager.freeId(entityId);
                if (componentList[componentType].Count == 0) {
                    componentList.Remove(componentType);
                }
            }
        }
    }

    public void addComponent<T>(int entityId, T component) where T : struct, IComponent {
        if (!componentList.ContainsKey(typeof(T)))
            componentList[typeof(T)] = new Dictionary<int, IComponent>();
        componentList[typeof(T)][entityId] = component;
    }

    public T getComponent<T>(int entityId) where T : struct, IComponent {
        if (componentList.TryGetValue(typeof(T), out var entityComponents))
            if (entityComponents.TryGetValue(entityId, out var component))
                return (T)component;
        return default;
    }

    public void setComponent<T>(int entityId, T component) where T : struct, IComponent {
        if (componentList.TryGetValue(typeof(T), out var entityComponents))
            if (entityComponents[entityId] != null)
                componentList[typeof(T)][entityId] = component;
    }

    public bool hasComponent<T>(int entityId) where T : struct, IComponent {
        if (componentList.ContainsKey(typeof(T)))
            return true;
        return false;
    }

    public IEnumerable<int> getEntitiesWithComponent<T>() where T : struct, IComponent {
        if (componentList.TryGetValue(typeof(T), out var entityComponents))
            return entityComponents.Keys;
        return Enumerable.Empty<int>();
    }

    public void clearEntities() {
        foreach (var component in componentList)
            component.Value.Clear();
        componentList.Clear();
        idManager.clearIds();
    }
}


    // public IEnumerable<int> getEntitiesWithComponents(params Type[] componentTypes) {
    //     if (componentTypes == null || componentTypes.Length == 0)
    //         return Enumerable.Empty<int>();

    //     foreach (var type in componentTypes) {
    //         if (!componentList.ContainsKey(type))
    //             return Enumerable.Empty<int>();
    //     }

    //     var smallestComponentSet = componentTypes
    //         .Select(type => componentList[type])
    //         .OrderBy(dict => dict.Count)
    //         .First();

    //     // Utilise un HashSet pour l'efficacité des opérations d'intersection
    //     var result = new HashSet<int>(smallestComponentSet.Keys);

    //     // Intersecte avec tous les autres ensembles de composants
    //     foreach (var type in componentTypes) {
    //         var currentComponents = componentList[type];
    //         result.IntersectWith(currentComponents.Keys);

    //         // Optimisation : si l'ensemble est vide, on peut arrêter tôt
    //         if (result.Count == 0)
    //             break;
    //     }
    // return result;
    // }
