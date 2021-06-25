# NEAPrototype-RAIL
 Prototyping an autonomous rail network for my NEA. AKA my mini project

# Description
 The aim of this project is to prototype my NEA - simulating an autonomous rail network.

# Requirements/Simulation Details
 * Trains will move in one direction around a circular track
 * This track will have stations on it
 * There is one central server
 * Tracks will:
	* Be connected to exclusively two stations
	* Will be a strictly straight line
 * Stations will:
	* Be connected by exclusively two tracks
	* Will have a transmitter broadcasting their station name, and this transmitter will have a range of 50 m 
 * Passengers will be instantiated at a station, then be given a target station and a specific train
	* A central server will register a request from the passenger to go to a target station
	* The server will then assign the passenger to a specific train and vice versa
	* The server will give the passenger a minimum time of arrival, and an ETA
 * Trains will:
	* Communicate with each other to prevent them from crashing
		* They will need a custom communications protocol with different signals corresponding to different scenarios
		* Transmitters will have a range of 100m
	* Need to be able to accelerate and decelerate
		* Accelerate from a station
		* Decelerate if needing to stop at a station to
			* Pick up somebody
			* Drop someone off
		* Decelerate to stop hitting a train in front
			* Train (when in a certain range to another train) will attempt to meet the same speed as the train in front
	* Communicate with the central server
 * All wireless communication will be instantaneous
	* (Add in error-detection and contingency)


## Train-Train protocol signals
 * ACK - acknowledgement
	* Self
	* Target train
 * STS - at station, stopped (global)
	* Station
 * STD - approaching station, decelerating (global)
	* Station
 * STC - approaching station, continuing (global)
	* Station

## Server-Train protocol signals
 * STACK - server-train acknowledgement
	* Target train
 * NPR - new passenger request
	* Target train
 * PDT - passenger data transfer
	* Target train
	* Passenger ID



## Train-Server protocol signals
 * SACK - server acknowledgement
	* Self
 * CLK - regular clock signal, updates the server on the position of the train
	* Self
	* Position
 * NPA - new passenger accept
	* Passenger
 * PSR - passenger removal; occurs when a passenger leaves the train
	* Passenger

## Station protocol signals
 * PRS - station presence signal
	* Self