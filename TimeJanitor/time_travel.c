#include "time_travel.h"
#include <stdio.h>

void init_time_buffer(TimeBuffer *tb) {
    tb->writeIndex = 0;
    tb->count = 0;
}

void record_state(TimeBuffer *tb, PlayerState *p, WorldState *w, unsigned int frame) {
    TimeSnapshot snapshot;
    snapshot.player = *p;
    snapshot.world = *w;
    snapshot.frameIndex = frame;

    tb->buffer[tb->writeIndex] = snapshot;
    tb->writeIndex = (tb->writeIndex + 1) % MAX_TIME_SNAPSHOTS;
    if (tb->count < MAX_TIME_SNAPSHOTS) {
        tb->count++;
    }
}

int rewind_state(TimeBuffer *tb, PlayerState *p, WorldState *w) {
    if (tb->count <= 0) {
        return 0; // No states left to rewind
    }

    // Go back one state
    tb->writeIndex = (tb->writeIndex - 1 + MAX_TIME_SNAPSHOTS) % MAX_TIME_SNAPSHOTS;
    tb->count--;

    TimeSnapshot snapshot = tb->buffer[tb->writeIndex];
    *p = snapshot.player;
    *w = snapshot.world;

    return 1; // Successfully rewound one state
}
