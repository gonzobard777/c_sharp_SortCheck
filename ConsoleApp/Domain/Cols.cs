namespace ConsoleApp.Domain;

/*
 * Таблица
 * по всем колонкам упорядоченная по возрастанию
 * выглядит следующим образом:
 * 
 * Col1  Col2  Col3
 *    1	    1	  1
 *    1	    1	  2
 *    1	    1	  3
 *    1	    2	  1
 *    1	    2	  2
 *    1	    2	  3
 *    2	    1	  1
 *    2	    1	  2
 *    2	    1	  3
 *    2	    2	  1
 *    2	    2	  2
 *    2	    2	  3
 *    3	    1	  1
 *    3	    1	  2
 *    3	    1	  3
 *    3	    2	  1
 *    3	    2	  2
 *    3	    2	  3
 */
public class Cols
{
    public int Id { get; set; }

    public int Col1 { get; set; }
    public int Col2 { get; set; }
    public int Col3 { get; set; }
}