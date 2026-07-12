#include "janitor.h"
#include <stdio.h>

void update_player_movement(PlayerState *p, float inputX, float inputY) {
    p->vx = inputX * 4.5f; // Platforming speed
    p->vy += inputY - 0.5f; // Simple gravity simulation
    p->x += p->vx;
    p->y += p->vy;

    // Boundary check
    if (p->y < 0.0f) {
        p->y = 0.0f;
        p->vy = 0.0;
    }

    if (inputX != 0.0f) {
        p->animState = 1; // Running animation
    } else {
        p->animState = 0; // Idle animation
    }
}

void sweep_temporal_anomaly(PlayerState *p, WorldState *w) {
    if (p->isCleaning && w->activeAnomalies > 0) {
        w->activeAnomalies--;
        w->score += 100;
        printf("🧹 Janitor clean up! Temporal anomaly scrubbed. Score: %d | Remaining Anomalies: %d\n", w->score, w->activeAnomalies);
    }
}
