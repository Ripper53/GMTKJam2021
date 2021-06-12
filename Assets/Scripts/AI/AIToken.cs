namespace AI {
    public class AIToken {
        public ArtificialIntelligence Source { get; }
        public float DeltaTime { get; }
        public bool Canceled { get; private set; } = false;

        public AIToken(ArtificialIntelligence source, float deltaTime) {
            Source = source;
            DeltaTime = deltaTime;
        }

        public void Cancel() => Canceled = true;

    }
}
