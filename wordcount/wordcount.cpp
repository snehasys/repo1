#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <limits.h>
#include <errno.h>
#include <chrono>
#include <iostream>
/* ----------------------------------------------------------
 * WordCount
 *
 * SYNOPSIS: reads a file and outputs a list of the words in
 * the file, and the number of times they occured.
 * Optionally can list only the most common (top 'n') words.
 * Words are delimited by whitespace; non-alphanumeric chars
 * are ignored.
 *
 * This 'wordcount' takes either one or two arguments:
 *
 *  $ wordcount.out < inputfile [<n>]
 *
 * ... where 'inputfile' is the file to read and count words
 * from, and 'n' (optional) is the number of words to output.
 *
 * Compile with: g++ -o wordcount.out wordcount.cpp */

#define MAXWORDSIZE 128
#define MAXMAPSIZE 10000

/* We'll use an array of wordcount structs to store our
 * running data.  The array will be terminated by an 'empty'
 * record, ie word[0] = 0; count = 0; */

struct wordcount {
   char word[MAXWORDSIZE];
   int  count;
   struct wordcount * next; // for seperate chaining on hash collision
};

wordcount * hashTable[MAXMAPSIZE] = {0};
int uniqueWords = 0;

unsigned int hashFunc(const char* word){
   unsigned int hashVal = 0;
   for (int i=0; i < strlen(word); ++i){
      hashVal += i * i + word[i] * word[i]; // i^2 + c^2 // skip hash 
   }

   return hashVal % MAXMAPSIZE; // the bucket id

}

/* ----------------------------------------------------------
 * addWord adds the word 'word' to our 'words' array; either
 * increments the count if it already exists, or reallocs the
 * array to create enough space to append the new word.
 * Assumes strlen(word) < MAXWORDSIZE */

// struct wordcount *addWord( struct wordcount *words, const char *word )
void addWord(const char *word )
{
   auto hf = hashFunc(word);
   if ( 0 == hashTable[hf] ) // unique word found
   {
      uniqueWords++;
      hashTable[hf] = new struct wordcount;
      strcpy(hashTable[hf]->word, word);
      hashTable[hf]->count = 1;
      hashTable[hf]->next = NULL;
   }
   else // hash collition may or happened
   {
      auto ptr = hashTable[hf];
      auto lastPtr = ptr;
      while( ptr != NULL){
         if (strcmp(ptr->word, word) == 0) // location found
         {
            ptr->count++;
            return;
         }
         lastPtr = ptr;
         ptr = ptr->next;
      }
      // insert newly collided item at the end of the list
      uniqueWords++;
      lastPtr->next = new struct wordcount; // (struct wordcount*) realloc(words, sizeof(*words)*(hf+2));
      strcpy(lastPtr->next->word, word);
      lastPtr->next->count = 1;
      lastPtr->next->next = NULL;
   }
}

struct wordcount * converter()
{
   struct wordcount * result = new wordcount[uniqueWords+1];

   for  (int i=0, j=0; i < MAXMAPSIZE; ++i)
   {
      wordcount* ptr = hashTable[i] ;
      while ( ptr != 0 )
      {
         result[j++] = *ptr;
         ptr = ptr->next;
      }
   }
   result[uniqueWords].count = 0;
   return result;
}

/* ----------------------------------------------------------
 * parseFile opens and reads the file named in 'filename'
 * Words are extracted, delimited by whitespace; non-alphanum
 * chars ignored. */

struct wordcount *parseFile()
{
   struct wordcount *words = 0;
   char curword[MAXWORDSIZE], *p;
   int  ch;

   /* Create an empty wordcount list */
   words = (struct wordcount*) malloc(sizeof(*words));
   words->count = 0; words->word[0] = 0;

   p = curword;
   while ((ch = fgetc(stdin)) != EOF) {
      if (isspace(ch) || ch == '\t' || ch == '\n' || ch == '\r') {
         /* we've reached the end of a word (or not started one yet) */
         if (p != curword) {
            *p = 0;
            // words = addWord( words, curword );
            addWord( curword );
            p = curword;
         }
      } else if (isalnum(ch)) {
         /* it's a valid character.  We truncate words that are too long */
         if (p-curword < MAXWORDSIZE-1) *p++ = (char)ch;
      }
      /* we ignore all other characters */
   }

   if (p != curword) addWord(curword);

   return converter();

}




int comparator( const void* left, const void * right)
{
   return (((struct wordcount*) left)->count) < (((struct wordcount*) right)->count) ;
}

/* ----------------------------------------------------------
 * Sorts the 'words' array in place, in reverse order (ie,
 * highest count at the beginning) */

void sortWordsList( struct wordcount *words )
{
   /* Use an insertion sort. qsort(3) would probably be quicker? */
   qsort( words, uniqueWords, sizeof(struct wordcount), comparator);

}



/* ----------------------------------------------------------
 * 'main': entry point
 *   Verifies and parses arguments
 *   Reads file and creates wordcount array
 *   Sorts array
 *   Prints results
 *   Cleans up
 */

int main( int argc, char *argv[] )
{
   int  n = 0, i;
   struct wordcount *words = 0;

   fscanf( stdin, "%u", &n );
   if(n < 0)  {
      n = INT_MAX;
   }

//   auto start = std::chrono::high_resolution_clock::now();
   /* Get the words list from the file */
   words = parseFile(); // After profiling, I have found this consumes most of the time

//   auto end = std::chrono::high_resolution_clock::now();
//   auto mSecsSoFar = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start);
   // std::cout << "ParseFile took: " <<  mSecsSoFar.count() << std::endl;
   
   //start = std::chrono::high_resolution_clock::now();
   /* sort the list of words */
   sortWordsList(words);
   //end = std::chrono::high_resolution_clock::now();
   //auto mSecsSoFar2 = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start);
   // std::cout << "sortWordsList took: " <<  mSecsSoFar2.count() << std::endl;
   
   //start = std::chrono::high_resolution_clock::now();
   /* print the top 'n' */
   for (i = 0; i < n; i++) {
      /* check if we've hit the end of the list */
      if (words[i].count == 0) break;

      printf("%d %s\n", words[i].count, words[i].word);
   }
//   end = std::chrono::high_resolution_clock::now();
//   auto mSecsSoFar3 = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start);
   // std::cout << "Printing " << n << " words took: " <<  mSecsSoFar3.count() << std::endl;

   /* clean up */
   free(words);

   return 0;
}
