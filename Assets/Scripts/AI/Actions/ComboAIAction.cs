namespace AI {
    public class ComboAIAction : AIAction {
        public AIAction[] Actions;

        public override void Execute(AIToken token) {
            foreach (AIAction action in Actions)
                action.Execute(token);
        }

    }
}
