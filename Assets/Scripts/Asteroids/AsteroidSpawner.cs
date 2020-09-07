using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _asteroidPrefab;
    private Vector3 _spawnPosition;
    private float _xMaxPosForSpawn;
    private float _xMinPosForSpawn;
    private float _yPosForSpawn;
    private float _zPosForSpawn;
    private float _spawnCD;
    private float _time;
    private int _maxAsteroidsCount;
    private int _asteroidsCount;
    private int _spawnType;
    private int _asteroidWidth;    

    public AnimationCurve CountAsteroidsForSpawn;

    private void Start()
    {
        _xMaxPosForSpawn = 13f;
        _xMinPosForSpawn = -13f;
        _yPosForSpawn = _player.transform.position.y;
        _zPosForSpawn = transform.position.z + 10f;
        _spawnCD = 2f;
        _time = 0;
        _maxAsteroidsCount = 11;
        _asteroidWidth = 2;
    }

    private void Update()
    {

        if ( _time >= _spawnCD)
        {
            _asteroidsCount = (int)CountAsteroidsForSpawn.Evaluate( Time.time);
            _spawnType = Random.Range( 1, 5);
            ChoosingTypeAndPositionOfAsteroidSpawn();
            _time = 0;
        }
        else
        {
            _time += Time.deltaTime;
        }
    }


    private void ChoosingTypeAndPositionOfAsteroidSpawn()
    {
        if ( _asteroidsCount < 3)
        {
            for ( int i = 0; i < _asteroidsCount; i++)
            {
                int StartPosXIndex = Random.Range( 0, _maxAsteroidsCount + 1);
                AsteroidSpawn(_xMinPosForSpawn + StartPosXIndex * _asteroidWidth);
            }
        }
        else
        {
            //_spawnType 1:  |*******    | 
            //_spawnType 2:  |   ********|
            //_spawnType 3:  |****   ****|
            //_spawnType 4:  |*** *** ****|
            if ( _spawnType == 1)
            {
                for ( int i = 0; i < _asteroidsCount; i++)
                {
                    AsteroidSpawn( _xMinPosForSpawn + i * _asteroidWidth);
                }
            }
            else if ( _spawnType == 2)
            {
                for ( int i = 0; i < _asteroidsCount; i++)
                {
                    AsteroidSpawn( _xMaxPosForSpawn - i * _asteroidWidth);
                }
            }
            else if ( _spawnType == 3)
            {
                int leftAsteroidLineCount = (int)_asteroidsCount / 2;
                int rightAsteroidLineCount = _asteroidsCount - leftAsteroidLineCount;
                for ( int i = 0; i < leftAsteroidLineCount; i++)
                {
                    AsteroidSpawn( _xMinPosForSpawn + i * _asteroidWidth);
                }
                for ( int i = 0; i < rightAsteroidLineCount; i++)
                {
                    AsteroidSpawn( _xMaxPosForSpawn - i * _asteroidWidth);
                }
            }
            else
            {
                int asteroidsCreated = 0;
                for (int i = 0; i < 13; i++)        // 13 becouse width of road is about 27, and width of 1 asteroid is 2
                {
                    if (i == 3 || i == 4 || i == 9 || i == 10)                  //create the way for spaceship
                    {
                        continue;
                    }
                    AsteroidSpawn(_xMinPosForSpawn + i * _asteroidWidth);
                    asteroidsCreated++;
                    if (asteroidsCreated == _asteroidsCount)
                    {
                        return;
                    }
                }
            }
        }
    }

    private void AsteroidSpawn(float xPosition)
    {
        _spawnPosition = new Vector3(xPosition, _yPosForSpawn, _zPosForSpawn);
        Instantiate(_asteroidPrefab, _spawnPosition, Quaternion.identity);
    }
}
