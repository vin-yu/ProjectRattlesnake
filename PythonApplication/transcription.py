from watson_developer_cloud import SpeechToTextV1
from azure.storage.blob import BlockBlobService, ContentSettings
from operator import itemgetter
import json
import os
import time
import pyodbc

block_blob_service = BlockBlobService(account_name='projectrattlesnakeblob', account_key='NPbjxaRiT7YZOeugFF0Rc8hIVyLMske2dn8JfizsVgBptitOwPjuTMqPC/+34EKNaJhNqY1g+jyPbkr0EzaiMA==')

server = 'projectrattlensake.database.windows.net'
database = 'ProjectRattlesnakeDB'
username = 'PRadmin@projectrattlensake'
password = 'Passw0rd!'
cnxn = pyodbc.connect('DRIVER={ODBC Driver 13 for SQL Server};SERVER='+server+';PORT=1443;DATABASE='+database+';UID='+username+';PWD='+ password)
cursor = cnxn.cursor()


def file_conversion(file_name):
    '''
    Uses the pydub library and ffmpeg to convert from .mp4 to .wav file.
    Skype outputs all recorded conversations as .mp4 files.
    '''
    from pydub import AudioSegment

    # Manually setting the converter: ffmpeg
    AudioSegment.converter = r"ffmpeg.exe"

    # Create a filename that is the same as the original .mp4 file
    wav_filename = file_name[:len(file_name)-4] + '.wav'

    # Creates a converted file in the same folder
    if file_name.endswith('.mp4'):
        AudioSegment.from_file(file_name)[:10000].export(wav_filename, format='wav')
    elif file_name.endswith('.mp3'):
        AudioSegment.from_mp3(file_name)[:10000].export(wav_filename, format='wav')
    return wav_filename


def transcription(AUDIO_FILE, folder, container):
    txtfile = AUDIO_FILE[:-4] + ".txt"
    speech_to_text = SpeechToTextV1(
        username='c7566986-a55d-4385-abde-101f455541dc',
        password='r5o7ADCsXMCO',
        x_watson_learning_opt_out=True
    )
    speech_to_text.get_model('en-US_BroadbandModel')
    with open(AUDIO_FILE, 'rb') as audio_file:
        response = speech_to_text.recognize(audio_file, content_type='audio/wav', timestamps=True, speaker_labels=True)
        list_results = response['results']
        speaker_labels = response['speaker_labels']
        speaker_count = 0
        file = open(txtfile, 'w')
        for result in list_results:
            sentence = ''
            before = -1
            timestamps = result['alternatives'][0]['timestamps']
            for timestamp in timestamps:
                word = timestamp[0]
                current_speaker = speaker_labels[speaker_count]['speaker']
                if(before == current_speaker or before == -1):
                    sentence += word + ' '
                    before = current_speaker
                else:
                    file.write('Speaker ' + str(current_speaker) + ': ' + sentence.capitalize()[:-1] + '.\n')
                    sentence = ''
                    sentence += word + ' '
                    before = current_speaker
                speaker_count += 1
            file.write('Speaker ' + str(current_speaker) + ': ' + sentence.capitalize()[:-1] + '.\n')
        audio_file.close()
        file.close()
        block_blob_service.create_blob_from_path(
            container,
            folder+'/'+txtfile,
            txtfile,
            content_settings=ContentSettings(content_type='text/plain')
        )
        time.sleep(5)
        os.remove(AUDIO_FILE)
        os.remove(txtfile)




container = "container"
while True:
    print ('Reading data from table')
    tsql = "UPDATE RattleSnakeTable SET Transcription_Status = 1 OUTPUT INSERTED.Audio_File, INSERTED.UserID, INSERTED.Upload_Time WHERE Transcription_Status = 0;"
    with cursor.execute(tsql):
        rows = cursor.fetchall()
        cursor.commit()
        rows.sort(key=itemgetter(2))
        for row in rows:
            audio_file = row[0]
            folder = row[1]
            #container = row[2]
            block_blob_service.get_blob_to_path(container, folder+'/'+audio_file, audio_file)
            oldAudio = None

            if not audio_file.endswith('.wav'):
                oldAudio = audio_file
                audio_file = file_conversion(oldAudio)

            print("starting ts")
            transcription(audio_file, folder, container)
            print("finished ts")

            txt_file = audio_file[:-4] + ".txt"
            if oldAudio is not None:
                os.remove(oldAudio)
                tsql = "UPDATE RattleSnakeTable SET Transcription_Status = 2, Transcription_File = ? WHERE Audio_File = ?"
                cursor.execute(tsql, txt_file, oldAudio)
            else:
                tsql = "UPDATE RattleSnakeTable SET Transcription_Status = 2, Transcription_File = ? WHERE Audio_File = ?"
                cursor.execute(tsql, txt_file, audio_file)
            
            cursor.commit()
    time.sleep(5)