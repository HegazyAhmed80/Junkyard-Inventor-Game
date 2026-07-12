CC = gcc
CFLAGS = -Wall -Wextra -std=c99
TARGET = time_janitor

all: $(TARGET)

$(TARGET): main.o time_travel.o janitor.o
	$(CC) $(CFLAGS) -o $(TARGET) main.o time_travel.o janitor.o

main.o: main.c time_travel.h janitor.h
	$(CC) $(CFLAGS) -c main.c

time_travel.o: time_travel.c time_travel.h
	$(CC) $(CFLAGS) -c time_travel.c

janitor.o: janitor.c janitor.h time_travel.h
	$(CC) $(CFLAGS) -c janitor.c

clean:
	rm -f *.o $(TARGET)
