#ifndef JANITOR_H
#define JANITOR_H

#include "time_travel.h"

void update_player_movement(PlayerState *p, float inputX, float inputY);
void sweep_temporal_anomaly(PlayerState *p, WorldState *w);

#endif
