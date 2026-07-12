#ifndef TIME_TRAVEL_H
#define TIME_TRAVEL_H

#define MAX_TIME_SNAPSHOTS 600 // Store up to 10 seconds of gameplay at 60 FPS

typedef struct {
    float x;
    float y;
    float vx;
    float vy;
    int animState;
    int isCleaning;
} PlayerState;

typedef struct {
    int activeAnomalies;
    int score;
} WorldState;

typedef struct {
    PlayerState player;
    WorldState world;
    unsigned int frameIndex;
} TimeSnapshot;

typedef struct {
    TimeSnapshot buffer[MAX_TIME_SNAPSHOTS];
    int writeIndex;
    int count;
} TimeBuffer;

void init_time_buffer(TimeBuffer *tb);
void record_state(TimeBuffer *tb, PlayerState *p, WorldState *w, unsigned int frame);
int rewind_state(TimeBuffer *tb, PlayerState *p, WorldState *w);

#endif
