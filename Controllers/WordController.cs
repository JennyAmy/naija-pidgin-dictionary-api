using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaijaPidginAPI.DTOs;
using NaijaPidginAPI.Entities;
using NaijaPidginAPI.Extentions;
using NaijaPidginAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WordController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        /// <summary>
        /// Adds new words that are not yet exisiting on the system
        /// </summary>>
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddWord(AddWordDTO wordDTO)
        {
            var word = mapper.Map<Word>(wordDTO);
            var userId = GetUserId();

            //Check if a word exists already
            if (await unitOfWork.WordRepository.WordAlreadyExists(wordDTO.WordInput, word.WordId))
                return BadRequest("Word already exists in the dictionary");

            //Automatically approves a word if the user is confirmed to be an admin
            if (await unitOfWork.UserRepository.UserIsAdmin(userId))
            {
                word.IsApproved = true;
                word.ApprovedBy = userId;
            }

            word.PostedBy = userId;
            unitOfWork.WordRepository.AddWord(word);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        /// <summary>
        /// Lists all words both approved and not yet approved
        /// </summary>>
        [HttpGet("list")]
        public async Task<IActionResult> GetAllWords()
        {
            var words = await unitOfWork.WordRepository.GetWordsAync();
            var wordDTO = mapper.Map<IEnumerable<ListWordDTO>>(words);
            return Ok(wordDTO);
        }

        /// <summary>
        /// List all approved words
        /// </summary>>
        [HttpGet("list-approved")]
        public async Task<IActionResult> GetApprovedWords()
        {
            var words = await unitOfWork.WordRepository.GetApprovedWordsAync();
            var wordDTO = mapper.Map<IEnumerable<ListWordDTO>>(words);
            return Ok(wordDTO);
        }

        /// <summary>
        /// List all not yet approved words
        /// </summary>>
        [HttpGet("list-not-approved")]
        public async Task<IActionResult> GetNotApprovedWords()
        {
            var words = await unitOfWork.WordRepository.GetNotApprovedWordsAync();
            var wordDTO = mapper.Map<IEnumerable<ListWordDTO>>(words);
            return Ok(wordDTO);
        }

        /// <summary>
        /// Get a word by Id
        /// </summary>>
        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetWordsById(int id)
        {
            var word = await unitOfWork.WordRepository.GetWordByIdAsync(id);
            var wordDTO = mapper.Map<ListWordDTO>(word);
            return Ok(wordDTO);
        }

        /// <summary>
        /// Get an approved word by Id
        /// </summary>>
        [HttpGet("list-approved/{id}")]
        public async Task<IActionResult> GetApprovedWordsById(int id)
        {
            var word = await unitOfWork.WordRepository.GetApprovedWordByIdAsync(id);
            var wordDTO = mapper.Map<ListWordDTO>(word);

            return Ok(wordDTO);
        }

        /// <summary>
        /// List all not yet approved words by Id
        /// </summary>>
        [HttpGet("list-not-approved/{id}")]
        public async Task<IActionResult> GetNotApprovedWordsById(int id)
        {
            var word = await unitOfWork.WordRepository.GetNotApprovedWordByIdAsync(id);
            var wordDTO = mapper.Map<ListWordDTO>(word);
            return Ok(wordDTO);
        }

        /// <summary>
        /// Lists all words added by a specified users Id
        /// </summary>>
        [HttpGet("list-by-userid")]
        [Authorize]
        public async Task<IActionResult> GetAllWordsByUserId()
        {
            var loggedInUserId = GetUserId();
            var words = await unitOfWork.WordRepository.GetAllWordsByUserIdAsync(loggedInUserId);

            var wordDTO = mapper.Map<IEnumerable<ListWordDTO>>(words);
            return Ok(wordDTO);
        }

        /// <summary>
        /// Updates a words
        /// </summary>>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateWord(int id, AddWordDTO wordDTO)
        {
            var word = await unitOfWork.WordRepository.GetWordByIdAsync(id);
            var userId = GetUserId();

            if (word == null)
                return NotFound();

            if (await unitOfWork.WordRepository.WordAlreadyExists(wordDTO.WordInput, word.WordId))
                return BadRequest("Word already exists in the dictionary");

            else
            {
                word.LastUpdatedBy = userId;
                word.LastUpdatedOn = DateTime.Now;
                mapper.Map(wordDTO, word);
                await unitOfWork.SaveAsync();
            }

            return StatusCode(200);
        }


        /// <summary>
        /// Approves a word
        /// </summary>>
        [HttpPut("approve/{id}")] //The plan is to modify only some part of the entity and not all
        [Authorize]
        public async Task<IActionResult> ApproveWord(int id)
        {
            var word = await unitOfWork.WordRepository.GetNotApprovedWordByIdAsync(id);
            var userId = GetUserId();

            if (word.IsApproved == false)
            {
                word.IsApproved = true;
            }

            word.IsApproved = false;
            word.ApprovedBy = userId;
            word.LastUpdatedOn = DateTime.Now;
            if (await unitOfWork.SaveAsync()) return StatusCode(200);

            return BadRequest("Some error occured, failed to approve word");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            unitOfWork.WordRepository.DeleteWord(id);
            await unitOfWork.SaveAsync();
            return Ok("Word has been deleted successfully");
        }

        /// <summary>
        /// Gives the total number of words in the system
        /// </summary>>
        [AllowAnonymous]
        [HttpGet("words-count")]
        public IActionResult TotalWordsCount()
        {
            int totalWords = unitOfWork.WordRepository.CountTotalWords();

            return Ok(new
            {
                status = true,
                data = totalWords
            });
        }

        /// <summary>
        /// Gives the total number of a logged in user words has added
        /// </summary>>
        [HttpGet("words-by-user-count")]
        [Authorize]
        public IActionResult TotalWordsByUserCount()
        {
            var userId = GetUserId();
            int totalWords = unitOfWork.WordRepository.CountTotalWordsByUserId(userId);

            return Ok(new
            {
                status = true,
                data = totalWords
            });
        }


        [HttpGet("approvedwords-by-user-count")]
        [Authorize]
        public IActionResult TotalApprovedWordsByUserCount()
        {
            var userId = GetUserId();
            int totalWords = unitOfWork.WordRepository.CountApprovedWordsByUserId(userId);

            return Ok(new
            {
                status = true,
                data = totalWords
            });
        }

        /// <summary>
        /// Checks if a word already exists 
        /// </summary>>
        [HttpGet("word-exists")]
        public async Task<IActionResult> WordExists(AddWordDTO wordDTO)
        {
            var word = mapper.Map<Word>(wordDTO);

            var result = await unitOfWork.WordRepository.WordAlreadyExists(wordDTO.WordInput, word.WordId);

            return result == true ? BadRequest("Word already exists in the dictionary") : StatusCode(200);

        }


        /// <summary>
        /// Searchs for words
        /// </summary>>
        [HttpPost("search")]
        public async Task<IActionResult> Search(string keyword)
        {

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return NotFound();
            }

            var words = await unitOfWork.WordRepository.SearchWords(keyword);
            var wordDTO = mapper.Map<IEnumerable<Word>>(words);
            return Ok(wordDTO);
                
        }
    }
}
