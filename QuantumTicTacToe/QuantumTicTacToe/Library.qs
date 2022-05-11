namespace QuantumTicTacToe {

    open Microsoft.Quantum.Convert;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Intrinsic;
    
    /// # Summary
    /// Gets the measured superposition value of a single qubit.
    /// # Output
    /// The Result of the measurement.
    operation GetQubitResult() : Result {
        use q = Qubit();
        // place kitty in box
        H(q);
        // did it die?
        return M(q);
    }

    /// # Summary
    /// Creates an array of Results of the given size that holds the measured superpositions.
    /// # Input
    /// ## size
    /// The size of the array to create.
    /// # Output
    /// An array of measured superpositions.
    operation GetQuantumPosition(size : Int) : Result[] {
        mutable qubits = [Zero, size = size];

        for i in 0..size - 1 {
            set qubits w/= i <- GetQubitResult();
        }

        return qubits;
    }
    
    operation GetMoves() : Result[][][] {
        return [
            // Moves
            [GetQuantumPosition(3), GetQuantumPosition(3), GetQuantumPosition(3), GetQuantumPosition(3)],
            // Order
            [GetQuantumPosition(2), GetQuantumPosition(2), GetQuantumPosition(2), GetQuantumPosition(2)]
        ];
    }
}

