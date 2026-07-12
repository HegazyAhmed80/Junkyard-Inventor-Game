#include <stdio.h>
#include <stdbool.h>
#include "time_travel.h"
#include "janitor.h"

void print_status(unsigned int tick, PlayerState *p, WorldState *w, const char* stateName) {
    printf("[%s] Tick %03u | Pos: (%.2f, %.2f) | Vel: (%.2f, %.2f) | Anim: %d | Score: %d\n",
           stateName, tick, p->x, p->y, p->vx, p->vy, p->animState, w->score);
}

int main() {
    printf("=========================================\n");
    printf("     TIME JANITOR - TEMPORAL ENGINE      \n");
    printf("=========================================\n\n");

    // Initialize states
    PlayerState player = { 0.0f, 0.0f, 0.0f, 0.0f, 0, 0 };
    WorldState world = { 5, 0 }; // 5 anomalies in the level
    TimeBuffer timeBuffer;
    init_time_buffer(&timeBuffer);

    unsigned int tick = 0;

    // --- Phase 1: Gameplay Progression (Recording Time) ---
    printf("--- PHASE 1: EXPLORING & CLEANING TIME ANOMALIES ---\n");
    for (tick = 1; tick <= 5; tick++) {
        // Mock input: moving right (1.0f) and jumping (1.5f at start)
        float moveInput = 1.0f;
        float jumpInput = (tick == 2) ? 5.0f : 0.0f;

        // Perform gameplay update
        update_player_movement(&player, moveInput, jumpInput);

        // At tick 4, clean an anomaly
        if (tick == 4) {
            player.isCleaning = 1;
            sweep_temporal_anomaly(&player, &world);
        } else {
            player.isCleaning = 0;
        }

        // Record the frame
        record_state(&timeBuffer, &player, &world, tick);
        print_status(tick, &player, &world, "RECORDING");
    }

    // --- Phase 2: Fatal Mistake (Trap Hit) ---
    printf("\n⚠️ WARNING: Player hit a temporal spike trap at Tick 6!\n");
    player.x = 999.0f; // Teleported to death zone
    player.y = -50.0f;
    player.animState = 9; // Dead
    print_status(6, &player, &world, "CRITICAL ERROR");

    // --- Phase 3: Rewind Mechanics ---
    printf("\n--- PHASE 3: INITIATING REWIND PROTOCOL ---\n");
    int rewindCount = 1;
    while (rewind_state(&timeBuffer, &player, &world)) {
        print_status(tick - rewindCount, &player, &world, "REWINDING");
        rewindCount++;
    }

    printf("\n🎉 Time rewound successfully to temporal anchor. Paradox resolved!\n");
    return 0;
}
