/**
 * Sean Skinner
 * Multilevel Feedback Queue Scheduler
 * CptS 370
 */

import java.util.*;

public class Scheduler extends Thread
{
    // private Vector queue;
	// Need to implement 3 different queuse for multilevel feedback therefore use a list
	private List<Vector> queues;
    private int timeSlice;
    private static final int DEFAULT_TIME_SLICE = 1000;

    // New data added to p161 
    private boolean[] tids; // Indicate which ids have been used
    private static final int DEFAULT_MAX_THREADS = 10000;

    // A new feature added to p161 
    // Allocate an ID array, each element indicating if that id has been used
    private int nextId = 0;
    private void initTid( int maxThreads ) {
	tids = new boolean[maxThreads];
	for ( int i = 0; i < maxThreads; i++ )
	    tids[i] = false;
    }

    // A new feature added to p161 
    // Search an available thread ID and provide a new thread with this ID
    private int getNewTid( ) {
	for ( int i = 0; i < tids.length; i++ ) {
	    int tentative = ( nextId + i ) % tids.length;
	    if ( tids[tentative] == false ) {
		tids[tentative] = true;
		nextId = ( tentative + 1 ) % tids.length;
		return tentative;
	    }
	}
	return -1;
    }

    // A new feature added to p161 
    // Return the thread ID and set the corresponding tids element to be unused
    private boolean returnTid( int tid ) {
	if ( tid >= 0 && tid < tids.length && tids[tid] == true ) {
	    tids[tid] = false;
	    return true;
	}
	return false;
    }

    // A new feature added to p161 
    // Retrieve the current thread's TCB from the queue
    public TCB getMyTcb( ) {
	Thread myThread = Thread.currentThread( ); // Get my thread object
	synchronized( queues ) { // Not sure what this is doing, saw originally it held queue, going to try just handing the list off to it, NOTE TO SELF: if this doesnt work try synching each queue in the MFQS
	    for(int j = 0; j < 3; j++){ // used to visit each queue in MFQS
			for ( int i = 0; i < queues.get(j).size( ); i++ ) { // works on each TCB in the current queue
				TCB tcb = ( TCB )queues.get(j).elementAt( i ); // grab element at i fron the jth queue and set it equal to tcb
				Thread thread = tcb.getThread( );
				if ( thread == myThread ) // if this is my TCB, return it
		    		return tcb;
	    	}
		}
	}
	return null;
    }

    // A new feature added to p161 
    // Return the maximal number of threads to be spawned in the system
    public int getMaxThreads( ) {
	return tids.length;
    }

    public Scheduler( ) {
	timeSlice = DEFAULT_TIME_SLICE;
	// queue = new Vector( );
	// Create mutliple queues instead of only 1 queue for MFQS
	queues = new ArrayList<Vector>();
	queues.add(new Vector());
	queues.add(new Vector());
	queues.add(new Vector());
	initTid( DEFAULT_MAX_THREADS );
    }

    public Scheduler( int quantum ) {
	timeSlice = quantum;
	// queue = new Vector( );
	// Create mutliple queues instead of only 1 queue for MFQS
	queues = new ArrayList<Vector>();
	queues.add(new Vector());
	queues.add(new Vector());
	queues.add(new Vector());
	initTid( DEFAULT_MAX_THREADS );
    }

    // A new feature added to p161 
    // A constructor to receive the max number of threads to be spawned
    public Scheduler( int quantum, int maxThreads ) {
	timeSlice = quantum;
	//queue = new Vector( );
	// Create mutliple queues instead of only 1 queue for MFQS
	queues = new ArrayList<Vector>();
	queues.add(new Vector());
	queues.add(new Vector());
	queues.add(new Vector());
	initTid( maxThreads );
    }

    private void schedulerSleep( ) {
	try {
	    Thread.sleep( timeSlice );
	} catch ( InterruptedException e ) {
	}
    }

    // A modified addThread of p161 example
    public TCB addThread( Thread t ) {
	// t.setPriority( 2 );
	TCB parentTcb = getMyTcb( ); // get my TCB and find my TID
	int pid = ( parentTcb != null ) ? parentTcb.getTid( ) : -1;
	int tid = getNewTid( ); // get a new TID
	if ( tid == -1)
	    return null;
	TCB tcb = new TCB( t, tid, pid ); // create a new TCB
	// queue.add( tcb );
	queues.get(0).add(tcb);
	return tcb;
    }

    // A new feature added to p161
    // Removing the TCB of a terminating thread
    public boolean deleteThread( ) {
	TCB tcb = getMyTcb( ); 
	if ( tcb!= null )
	    return tcb.setTerminated( );
	else
	    return false;
    }

    public void sleepThread( int milliseconds ) {
	try {
	    sleep( milliseconds );
	} catch ( InterruptedException e ) { }
    }
    
    // A modified run of p161
    public void run( ) {
	Thread current = null;
	
	// keep track of iterations being gone through for queue 1 and 2, queue 0 doesn't need to keep track as it will always pass TCBs to next queue.
	int q1count = 0;
	int q2count = 0;

	// this.setPriority( 6 );
	
	while ( true ) {

		int priorityqueue;

		// Check if q0 is empty, if it isnt work on q0
		if(queues.get(0).isEmpty() == false){
			priorityqueue = 0;
		}
		// Check if q1 is empty, if it isnt work on q1
		else if(queues.get(1).isEmpty() == false){
			priorityqueue = 1;
		}
		// if q1 and q0 are empty work on q2
		else{
			priorityqueue = 2;
		}
		
	    try {
		// get the next TCB and its thread
		if ( queues.get(priorityqueue).size( ) == 0 )
		    continue;
		TCB currentTCB = (TCB)queues.get(priorityqueue).firstElement( );
		if ( currentTCB.getTerminated( ) == true ) {
		    queues.get(priorityqueue).remove( currentTCB );
		    returnTid( currentTCB.getTid( ) );
		    continue;
		}
		current = currentTCB.getThread( );
		if ( current != null ) {
		    if ( current.isAlive( ) ){
				// current.setPriority( 4 );
				current.resume();
				// Wait in intervals of 500ms aka queue 0s thread execution time
				sleepThread(timeSlice/2);
				// keep track of iterations thread has gone through.
				if(priorityqueue == 1){
					q1count++;
				}
				else if(priorityqueue == 2){
					q2count++;
				}
			}
		    else {
			// Spawn must be controlled by Scheduler
			// Scheduler must start a new thread
			current.start( ); 
			// current.setPriority( 4 );
		    }
		}
		
		// schedulerSleep( );
		// System.out.println("* * * Context Switch * * * ");

		synchronized ( queues ) {
		    if ( current != null && current.isAlive( ) ){
				// current.setPriority( 2 );
				//current.suspend();
		    	//queue.remove( currentTCB ); // rotate this TCB to the end
		    	//queue.add( currentTCB );

				// If cant finish in half a time cycle switch to queue 1
				if(priorityqueue == 0){
					switchQueue(priorityqueue, currentTCB);
				}
				// if in queue 1 and couldnt finish in 1 time cycle, pass to queue 2
				else if(priorityqueue == 1 && q1count % 2 == 0){
					switchQueue(priorityqueue, currentTCB);
				}
				// If in queue 2 and couldnt finish in two time cyclers, put at then end of queue 2
				else if(priorityqueue == 2 && q2count % 4 == 0){
					switchQueue(priorityqueue, currentTCB);
				}
			}
		}
	    } catch ( NullPointerException e3 ) { };
	}
    }


	// New method to change the queue a TCB is in if it takes too long when attempting to execute
	public void switchQueue(int queuelevel, TCB t){
		// If could not finish execution in queue 0 send it to queue 1
		if(queuelevel == 0){
			queues.get(0).remove(t);
			queues.get(1).add(t);
		}
		// If could not finish execution in queue 1 send it to queue 2
		else if(queuelevel == 1){
			queues.get(1).remove(t);
			queues.get(2).add(t);
		}
		// If could not finish execution in queue 2 send it to the back of queue 2
		else{
			queues.get(2).remove(t);
			queues.get(2).add(t);
		}
	}
}
